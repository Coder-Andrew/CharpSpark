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
    var toolbarOptions = [
        ['bold', 'italic', 'underline', 'strike'],        // toggled buttons
        ['blockquote'],
        [{ 'list': 'ordered' }, { 'list': 'bullet' }],
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        [{ 'color': [] }, { 'background': [] }],          // dropdown with defaults from theme
        [{ 'align': [] }],
        ['link'],
        ['clean']                                         // remove formatting button
    ]
    // var quill = new Quill('#editor', {
    //     theme: 'snow',
    //     modules: {
    //         toolbar: toolbarOptions,
    //     }
    // });
    var quill1 = new Quill('#editor1', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions,
        },
        readOnly: true
    });

    var quill2 = new Quill('#editor2', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions,
        },
        readOnly: true
    });
    console.log("Editor initialized");
    // const resumeContent = document.getElementById("resume-content").value;
    // const resumeArea = document.getElementById("resume-area");
    // resumeArea.innerHTML = decodeURIComponent(resumeContent);
    // const delta = quill.clipboard.convert(resumeArea.innerHTML);
    // quill.setContents(delta);

    const resumeContent1 = document.getElementById("resume-content1").value;
    const resumeArea1 = document.getElementById("resume-area1");
    resumeArea1.innerHTML = decodeURIComponent(resumeContent1);
    const delta1 = quill1.clipboard.convert(resumeArea1.innerHTML);
    quill1.setContents(delta1);

    const resumeContent2 = document.getElementById("resume-content2").value;
    const resumeArea2 = document.getElementById("resume-area2");
    resumeArea2.innerHTML = decodeURIComponent(resumeContent2);
    const delta2 = quill2.clipboard.convert(resumeArea2.innerHTML);
    quill2.setContents(delta2);

    const saveBtn = document.getElementById("save-resume");
    saveBtn.addEventListener('click', () => getHtmlInfo(), false);
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
        themeStylesheet.setAttribute('href', '/css/ViewResumeLight.css');
        themeLabel.textContent = 'Switch to Dark Mode';
    } else {
        themeStylesheet.setAttribute('href', '/css/ViewResumeDark.css');
        themeLabel.textContent = 'Switch to Light Mode';
    }
}

async function exportPdf(quill) {
    console.log("Export PDF button clicked");
    const validationArea = document.getElementById('validation-area');
    const successValidation = document.getElementById('validation-success');
    const errorValidation = document.getElementById('validation-error');
    var title = document.getElementById('resume-title').value;
    if (title === "" || title === null || title === "Resume Title") {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `Please enter a valid title for your resume. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
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
        cloneSuccess.innerHTML = `Resume exported successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
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

async function getHtmlInfo() 
{
    const validationArea = document.getElementById('validation-area');
    const successValidation = document.getElementById('validation-success');
    const errorValidation = document.getElementById('validation-error');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const whiteSpacePattern = /^\s+$/;
    const title = document.getElementById('resume-title').value;
    if (title === "" || title === null || title === "Resume Title" || title.match(specialPattern) || title.match(whiteSpacePattern) || title.length > 99)
    {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `Resume Title is invalid, please use a valid title. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        return;
    }
    console.log("Save Resume button clicked");
    const htmlContent = document.querySelector('.ql-editor').innerHTML;
    const resumeId = document.getElementById('resume-id').value;
    const response = await fetch(`/api/resume/${resumeId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify({
            resumeId: resumeId,
            title: title,
            htmlContent: encodeURIComponent(htmlContent)
        })
    });    
    if (response.ok)
    {
        validationArea.innerHTML = "";
        const cloneSuccess = successValidation.cloneNode(true);
        cloneSuccess.style.display = "block";
        cloneSuccess.innerHTML = `Resume saved successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneSuccess);
        return;
    }
    else
    {
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `An error occurred while saving the resume. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        console.log("Error saving information");
        return;
    }
}