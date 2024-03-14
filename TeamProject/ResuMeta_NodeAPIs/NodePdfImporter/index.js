import express from 'express';
import multer from 'multer';
import PDFParser from 'pdf2json';
import { extractTextByLines } from './extractText.js';

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

