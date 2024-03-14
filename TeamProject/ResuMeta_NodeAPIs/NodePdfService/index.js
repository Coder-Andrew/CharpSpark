const multer = require('multer');
const PDFParser = require('pdf2json');
const { extractTextByLines } = require('./extractText.js');
const puppeteer = require('puppeteer');
const express = require('express');

const app = express();
const PORT = process.env.PORT || 8080;

// Set up temp multipart form storage for file uploads
const storage = multer.memoryStorage();
const upload = multer({ storage: storage });

app.use(express.json());

app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});

app.post('/pdfgenerator', async (req, res) => {
    var httpContent = req.body;
    var jsonContent = httpContent.html;
    var html = JSON.parse(jsonContent);
    const delta = decodeURIComponent(html.htmlContent);
    const pdf = await genPDFWithPuppeteer(delta);
    if (pdf) {
        res.json({pdf: pdf});
    }
    else {
        res.status(500).send('Error generating PDF');
    }
});

app.post('/importPdf', upload.single('pdfFile'), async (req, res) => {
    if (!req.file) {
      return res.status(400).send('No file uploaded.');
    }
  
    const pdfParser = new PDFParser();
    pdfParser.on("pdfParser_dataError", errData => console.error(errData.parserError));
    pdfParser.on("pdfParser_dataReady", pdfData => {
      const parsedData = extractTextByLines(pdfData);
  
      const response = {
          filename: req.file.originalname,
          content: parsedData
      }
      res.send(response);
    });
  
    // Load the PDF file from the uploaded buffer
    pdfParser.parseBuffer(req.file.buffer);
  });

async function genPDFWithPuppeteer(delta) {
    const browser = await puppeteer.launch();
    const page = await browser.newPage();
    await page.setContent(delta);
    await page.addStyleTag({content: 'body { font-family: Arial, sans-serif; font-size: 10px; }'});
    const pdf = await page.pdf({format: 'A4'});
    const base64Pdf = Buffer.from(pdf).toString('base64');
    await browser.close();
    return base64Pdf;
}