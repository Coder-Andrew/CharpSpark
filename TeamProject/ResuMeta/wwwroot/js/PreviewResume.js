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