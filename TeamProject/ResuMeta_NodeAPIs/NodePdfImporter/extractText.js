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