document.addEventListener("DOMContentLoaded", loadQuill, false);

function loadQuill() {
    console.log("Loading Quill");
    var script = document.createElement('script');
    script.src = "https://cdn.quilljs.com/1.0.0/quill.js";
    document.head.appendChild(script);
    script.onload = function () {
        console.log("Quill loaded");
        initializePage();
    }

}

function initializePage() {

    var quill1 = new Quill('#editor1', {
        theme: 'snow',
        readOnly: true
    });

    var quill2 = new Quill('#editor2', {
        theme: 'snow',
        readOnly: true
    });
    console.log("Editor initialized");

    const coverLetterContent1 = document.getElementById("cover-letter-content1").value;
    const coverLetterArea1 = document.getElementById("cover-letter-area1");
    coverLetterArea1.innerHTML = decodeURIComponent(coverLetterContent1);
    const delta1 = quill1.clipboard.convert(coverLetterArea1.innerHTML);
    quill1.setContents(delta1);

    const coverLetterId = document.getElementById('cover-letter-id').value;
    const coverLetterContent2 = fetchAndDisplayData(coverLetterId, quill2);
    const coverLetterArea2 = document.getElementById("cover-letter-area2");
    coverLetterArea2.innerHTML = decodeURIComponent(coverLetterContent2);
    const delta2 = quill2.clipboard.convert(coverLetterArea2.innerHTML);
    quill2.setContents(delta2);

    const regenerateButton = document.getElementById("regenerate-button");
    if (regenerateButton) {
        regenerateButton.addEventListener("click", () => {
            const coverLetterId = document.getElementById('cover-letter-id').value;
            fetchAndDisplayData(coverLetterId, quill2);
        });
    }

    const saveBtn = document.getElementById("save-and-apply");
    saveBtn.addEventListener('click', async () => {
        await getHtmlInfo();
        window.location.href = `/Resume/YourDashboard`;
    }, false);
    const exportBtn = document.getElementById("export-pdf");
    exportBtn.addEventListener('click', () => exportPdf(quill2), false);
    const themeSwitcher = document.getElementById('theme-switcher');
    themeSwitcher.addEventListener('click', SwitchTheme, false);
}

async function SwitchTheme() {
    var themeStylesheet = document.getElementById('theme-stylesheet');
    var themeSwitcher = document.getElementById('theme-switcher');
    var themeLabel = document.getElementById('theme-label');
    if (themeSwitcher.checked) {
        themeStylesheet.setAttribute('href', '/css/ViewImproveCoverLetterLight.css');
        themeLabel.textContent = 'Switch to Dark Mode';
    } else {
        themeStylesheet.setAttribute('href', '/css/ViewImproveCoverLetterDark.css');
        themeLabel.textContent = 'Switch to Light Mode';
    }
}

async function exportPdf(quill) {
    console.log("Export PDF button clicked");
    const validationArea = document.getElementById('validation-area');
    const successValidation = document.getElementById('validation-success');
    const errorValidation = document.getElementById('validation-error');
    var title = document.getElementById('cover-letter-title').value;
    if (title === "" || title === null || title === "Cover Letter Title") {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `Please enter a valid title for your cover letter. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        return;
    }

    const html = quill.root.innerHTML;
    const htmlCoded = encodeURIComponent(html);

    const response = await fetch(`/api/exportPdf/`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            htmlContent: htmlCoded
        })
    });
    if (response.ok) {
        console.log("PDF generated successfully");
        let jsonString = await response.text();
        let base64Pdf = jsonString
        let pdfBytes = atob(base64Pdf);
        let pdfArray = new Uint8Array(new ArrayBuffer(pdfBytes.length));

        for (let i = 0; i < pdfBytes.length; i++) {
            pdfArray[i] = pdfBytes.charCodeAt(i);
        }

        const blob = new Blob([pdfArray], { type: 'application/pdf' });
        const url = URL.createObjectURL(blob);
        const link = document.createElement('a');
        link.href = url;
        link.download = `${title}.pdf`;
        document.body.appendChild(link);
        validationArea.innerHTML = "";
        const cloneSuccess = successValidation.cloneNode(true);
        cloneSuccess.style.display = "block";
        cloneSuccess.innerHTML = `Cover letter exported successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneSuccess);
        link.click();
        document.body.removeChild(link);
        return;
    }
    else {
        console.log("Error generating PDF");
        return;
    }
}

async function getHtmlInfo() {
    const validationArea = document.getElementById('validation-area');
    const successValidation = document.getElementById('validation-success');
    const errorValidation = document.getElementById('validation-error');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const whiteSpacePattern = /^\s+$/;
    const title = document.getElementById('cover-letter-title').value;
    if (title === "" || title === null || title === "Cover Letter Title" || title.match(specialPattern) || title.match(whiteSpacePattern) || title.length > 99) {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `Cover letter Title is invalid, please use a valid title. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        return;
    }
    console.log("Save Cover Letter button clicked");
    const htmlContent = document.getElementById('editor2').querySelector('.ql-editor').innerHTML;
    const coverLetterId = document.getElementById('cover-letter-id').value;
    const response = await fetch(`/api/coverletter/${coverLetterId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            coverLetterId: coverLetterId,
            title: title,
            htmlContent: encodeURIComponent(htmlContent)
        })
    });
    if (response.ok) {
        // validationArea.innerHTML = "";
        // const cloneSuccess = successValidation.cloneNode(true);
        // cloneSuccess.style.display = "block";
        // cloneSuccess.innerHTML = `Cover letter saved successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        // validationArea.appendChild(cloneSuccess);
        return;
    }
    else {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `An error occurred while saving the cover letter. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        console.log("Error saving information");
        return;
    }
}


async function fetchAndDisplayData(coverLetterId, quill2) {
    const loadingScreen = document.getElementById("loading-screen");
    loadingScreen.style.display = "block";

    quill2.root.innerHTML = "Loading...";

    try {
        const response = await fetch(`/api/cgpt/improve-coverletter/${coverLetterId}`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-type': 'application/json; charset=UTF-8'
            }
        });

        if (!response.ok) {
            throw new Error('Failed to fetch data');
        }

        let responseData;
        try {
            responseData = await response.text();
        } catch (error) {
            throw new Error('Failed to parse response data');
        }

        quill2.root.innerHTML = responseData;

    } catch (error) {
        console.error('Error fetching and displaying data:', error);
        quill2.root.innerHTML = "Error loading data";
    } finally {
        loadingScreen.style.display = "none"; 
    }
}
