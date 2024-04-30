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

    var quill = new Quill('#editor', {
        readOnly: true,
        theme: 'snow'
    });

    var previewQuill = new Quill('#preview-editor', {
        readOnly: true,
        theme: 'snow'
    });

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

    //Preview Template
    const previewContent = document.getElementById("preview-content").value;
    const previewArea = document.getElementById("preview-area");
    previewArea.innerHTML = decodeURIComponent(previewContent);
    const previewDelta = previewQuill.clipboard.convert(previewArea.innerHTML);
    previewQuill.setContents(previewDelta);

    const themeSwitcher = document.getElementById('theme-switcher');
    themeSwitcher.addEventListener('click', SwitchTheme, false);
    const previewBtn = document.getElementById('preview-resume');
    previewBtn.addEventListener('click', () => PreviewResume(), false);
    const closePreviewBtn = document.getElementById('close-preview');
    closePreviewBtn.addEventListener('click', () => closePreview(), false);
    const saveAndApplyBtn = document.getElementById('save-and-apply');
    saveAndApplyBtn.addEventListener('click', () => getHtmlInfo(), false);
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
        themeStylesheet.setAttribute('href', '/css/PreviewResumeLight.css');
    } else {
        themeStylesheet.setAttribute('href', '/css/PreviewResumeDark.css');
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
    
    const previewEditor = document.querySelector('#preview-editor');
    const htmlContent = previewEditor.querySelector('.ql-editor').innerHTML;
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
        // Redirect to the YourResume page
        window.location.href = `/Resume/YourResume/${resumeId}`;
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