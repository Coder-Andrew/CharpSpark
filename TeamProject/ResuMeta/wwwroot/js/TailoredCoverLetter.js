
document.addEventListener("DOMContentLoaded", loadQuill, false);

const htmlContent = decodeURIComponent(sessionStorage.getItem('htmlContent'));

function loadQuill() {

    var script = document.createElement('script');
    script.src = "https://cdn.quilljs.com/1.0.0/quill.js";
    document.head.appendChild(script);
    script.onload = function () {
        console.log("Quill loaded");
        initializePage();
    }

}

function initializePage() {
    window.addEventListener('beforeunload', function (event) {
        //sessionStorage.clear(); // might want to only clear jobLink from session storage
    });

    var quill2 = new Quill('#editor2', {
        theme: 'snow',
        readOnly: true
    });

    const htmlContent = decodeURIComponent(sessionStorage.getItem('htmlContent'));
    if (htmlContent) {
        quill2.root.innerHTML = htmlContent;
    }
    sessionStorage.clear();

    const saveBtn = document.getElementById("save-cover-letter");
    saveBtn.addEventListener('click', async () => {
        await getHtmlInfoToSave();
    }, false);
}


async function getHtmlInfoToSave() {
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
        cloneError.innerHTML = `Cover Letter Title is invalid, please use a valid title. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        return;
    }
    console.log("Save Cover Letter button clicked");
    hiringManager = "Hiring Manager";
    let coverLetterHtml = document.getElementById('editor2').querySelector('.ql-editor').innerHTML;
    coverLetterHtml = coverLetterHtml.replace(/\n/g, '<br>');
    const coverLetterInfo = {
        Id: document.getElementById('userId').value,
        HiringManager: hiringManager,
        Body: htmlContent,
        title: title,
        htmlContent: encodeURIComponent(coverLetterHtml)
    };
    const response = await fetch('/api/coverletter/tailoredcoverletter', {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(coverLetterInfo)
    });
    if (response.ok) {
        const data = await response.json();

        validationArea.innerHTML = "";
        const cloneSuccess = successValidation.cloneNode(true);
        cloneSuccess.style.display = "block";
        cloneSuccess.innerHTML = `Cover Letter saved successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneSuccess);
        window.location.href = `/Resume/YourDashboard`;
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