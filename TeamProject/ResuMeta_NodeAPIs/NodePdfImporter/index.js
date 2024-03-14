import express from 'express';
import multer from 'multer';
import PDFParser from 'pdf2json';

const app = express();
const port = 9090;

const storage = multer.memoryStorage();
const upload = multer({ storage: storage });

app.listen(port, () => {
  console.log(`App is running on port: ${port}`);
});

app.get('/', (req, res) => {
  const message = { "test": 'Hello World!' };
  console.log(`request received from ip: ${req.ip}, message: ${message}`);
  res.send(message);
});

app.post('/api/importPdf', upload.single('pdfFile'), async (req, res) => {
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

function extractTextByLines(pdfData) {
  const page = pdfData.Pages[0]; // This assumes that the PDF has only one page
  let lines = [];
  let currentLineY = null;
  let currentLine = [];

  page.Texts.forEach(text => {
    if (currentLineY === null || Math.abs(currentLineY - text.y) > 0.5) { 
      if (currentLine.length > 0) {
        lines.push(currentLine);
        currentLine = [];
      }
      currentLineY = text.y;
    }
    currentLine.push({
      text: decodeURIComponent(text.R[0].T),
      style: text.R[0].TS
    });
  });

  if (currentLine.length > 0) {
    lines.push(currentLine);
  }

  return lines.map(line => line.map(textObject => ({
    text: textObject.text,
    style: textObject.style.map(style => Math.floor(style))
  })));
}

export { extractTextByLines };