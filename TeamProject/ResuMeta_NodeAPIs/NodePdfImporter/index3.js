import express from 'express';
import multer from 'multer';
import * as pdfjsLib from 'pdfjs-dist/legacy/build/pdf.mjs';
import * as pdf2json from 'pdf2json';
//const fs = require('fs');
//const pdfjsLib = require('pdfjs-dist/legacy/build/pdf.mjs');

const app = express();
const port = 9090;

const storage = multer.memoryStorage();
const upload = multer({ storage: storage });


app.listen(port, () => {
    console.log(`App is running on port: ${port}`);
});

app.get('/', (req, res) => {
  const message = {"test":'Hello World!'}
  console.log(`request recieved from ip: ${req.ip}, message: ${message}`);
  res.send(message);
});

app.post('/api/importPdf', upload.single('pdfFile'), async (req, res) => {
  if (req.file) {
    try {
      const pdfBuffer = req.file.buffer;
      const pdfText = await pdfJsParse(pdfBuffer);
      console.log(pdfText);
      res.send(pdfText);
    } catch (err) {
      console.log(err);
      res.status(500).send('Error processing the file');
    }
  } else {
    res.status(400).send('No file uploaded.');
  }
});

// Left off trying to figure out how to import a pdf file or handle one through a request.
// checkout multer for handling file uploads



// let exampleJson = 

// {'pdfContent': [
//   { 'content': {
//     'header': 'This is an example header',
//     'body': 'This is an example body. This will hold any descriptions but can be set to null'
//   }},
//   { 'content': {
//     'header': 'This is an example header',
//     'body': 'This is an example body. This will hold any descriptions but can be set to null'
//   }}
// ]}


async function pdfJsParse(pdfBuffer) {
  try {
    const pdfData = new Uint8Array(pdfBuffer);
    const pdfDocument = await pdfjsLib.getDocument({data: pdfData}).promise;
  
    for (let pageNum = 1; pageNum < pdfDocument.numPages; pageNum++) {
      const page = await pdfDocument.getPage(pageNum);
      const content = await page.getTextContent();
  
      let pageText = '';
      content.items.forEach((item) => {
        pageText += item.str + ' ';
      });
  
      console.log(pageText);
      return pageText;
    }
    console.log(pdfDocument.numPages);
  
    // For example, let's just send back a confirmation for now
    return { message: 'File received and processed', fileName: req.file.originalname };
  }
  catch (err) {
      console.log(err);
  }
}

