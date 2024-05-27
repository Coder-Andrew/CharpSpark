document.addEventListener("DOMContentLoaded", loadQuill, false);


function loadQuill() {
    console.log("Loading Quill");
    var script = document.createElement('script');
    script.src = "https://cdn.quilljs.com/1.0.0/quill.js";
    document.head.appendChild(script);
    script.onload = function () {
        console.log("Quill loaded");

        //Register the DividerBlot
        const BlockEmbed = Quill.import('blots/block/embed');

        class DividerBlot extends BlockEmbed {
        static create() {
            let node = super.create();
            return node;
        }

        static formats() {
            return true;
        }
        }

        DividerBlot.blotName = 'divider';
        DividerBlot.tagName = 'hr';

        Quill.register(DividerBlot);
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
        ['clean'],                                         // remove formatting button
        ['divider']
    ]
    
    var quill = new Quill('#editor', {
        theme: 'snow',
        modules: {
            toolbar: {
                container: toolbarOptions,
                handlers: {
                    'divider': function() {
                        let range = quill.getSelection(true);
                        quill.insertText(range.index, '\n', Quill.sources.USER);
                        quill.insertEmbed(range.index + 1, 'divider', true, Quill.sources.USER);
                        quill.setSelection(range.index + 2, Quill.sources.SILENT);
                    }
                }
            },
        }
    });

    console.log("Editor initialized");

    //Templates
    for (let i = 1; i <= 5; i++) {
        var templateEditor = document.getElementById(`template${i}-editor`);
        var templatequill = new Quill(`#template${i}-editor`, {
            readOnly: true,
            theme: 'snow'
        });
    
        var templatecontent = document.getElementById(`template${i}-content`).value;
        var templatearea = document.getElementById(`template${i}-area`);
        templatearea.innerHTML = decodeURIComponent(templatecontent);
    
        var templatedelta = quill.clipboard.convert(templatearea.innerHTML);
        templatequill.setContents(templatedelta);
    }

    //Resume
    const resumeContent = document.getElementById("resume-content").value;
    const resumeArea = document.getElementById("resume-area");
    resumeArea.innerHTML = decodeURIComponent(resumeContent);
    const delta = quill.clipboard.convert(resumeArea.innerHTML);
    quill.setContents(delta);
    const saveBtn = document.getElementById("save-resume");
    saveBtn.addEventListener('click', () => getHtmlInfo(), false);
    const deleteBtn = document.getElementById("delete-resume");
    deleteBtn.addEventListener('click', () => deleteResume(), false);
    const exportBtn = document.getElementById("export-pdf");
    exportBtn.addEventListener('click', () => exportPdf(quill), false);
    const themeSwitcher = document.getElementById('theme-switcher');
    themeSwitcher.addEventListener('click', SwitchTheme, false);
    const improveWithAiBtn = document.getElementById("improve-with-ai");
    improveWithAiBtn.addEventListener("click", redirectToAiPage);
    const previewBtn = document.getElementById('preview-resume');
    previewBtn.addEventListener('click', () => PreviewResume(), false);
    const closePreviewBtn = document.getElementById('close-preview');
    closePreviewBtn.addEventListener('click', () => closePreview(), false);
    document.getElementById('generate-careers').addEventListener('click', generateCareers);
}

async function PreviewResume() {
    const templates = document.getElementById('templates');
    const templatesTitle = document.getElementById('templates-title');
    templates.style.display = 'flex';
    templatesTitle.style.display = 'block';
    document.body.classList.add('show-before');
}

async function closePreview() {
    const templates = document.getElementById('templates');
    const templatesTitle = document.getElementById('templates-title');
    templates.style.display = 'none';
    templatesTitle.style.display = 'none';
    document.body.classList.remove('show-before');

}

async function SwitchTheme() {
    var themeStylesheet = document.getElementById('theme-stylesheet');
    var themeSwitcher = document.getElementById('theme-switcher');
    var themeLabel = document.getElementById('theme-label');
    if (themeSwitcher.checked) {
        themeStylesheet.setAttribute('href', '/css/ViewResumeLight.css');
    } else {
        themeStylesheet.setAttribute('href', '/css/ViewResumeDark.css');
    }
}

async function deleteResume() {
    const validationArea = document.getElementById('validation-area');
    const successValidation = document.getElementById('validation-success');
    const errorValidation = document.getElementById('validation-error');
    const resumeId = document.getElementById('resume-id').value;

    const userConfirmed = confirm('Are you sure you want to delete your resume?');
    if (!userConfirmed) {
        return;
    }

    const response = await fetch(`/api/resume/${resumeId}`, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok) {
        console.log("Resume deleted successfully");
        validationArea.innerHTML = "";
        const cloneSuccess = successValidation.cloneNode(true);
        cloneSuccess.style.display = "block";
        cloneSuccess.innerHTML = `Resume deleted successfully. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneSuccess);
        window.location.href = "/Resume/YourDashboard";
        return;
    }
    else {
        console.log("Error deleting resume");
        validationArea.innerHTML = "";
        const cloneError = errorValidation.cloneNode(true);
        cloneError.style.display = "block";
        cloneError.innerHTML = `An error occurred while deleting the resume. <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close" style="float:right;"></button>`;
        validationArea.appendChild(cloneError);
        return;
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

function redirectToAiPage() {
    const resumeId = document.getElementById("resume-id").value;
    const aiPageUrl = `/Resume/ImproveResume/${resumeId}`;
    console.log("Redirecting to improve with ai page");
    // Redirect the user to improve with ai page
    window.location.href = aiPageUrl;
}

async function generateCareers() {
    const id = document.getElementById("resume-id").value;
    const modalBody = document.querySelector('#help-modal .modal-body');
    // Show loading spinner
    modalBody.innerHTML = '<div class="spinner-border" role="status"><span class="sr-only">Loading...</span></div>';
    const helpModal = new bootstrap.Modal(document.getElementById('help-modal'));
    helpModal.show();

    const response = await fetch(`/api/cgpt/generatecareers/${id}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        }
    });

    if (!response.ok) {
        console.error(`HTTP error! status: ${response.status}`);
        modalBody.innerText = "Error fetching career suggestions. Please try again later.";
    } else {
        const data = await response.text();
        console.log(data);
        const formattedData = data.split('\n').join('<br>');
        modalBody.innerHTML = formattedData;
    }
}
