document.addEventListener("DOMContentLoaded", loadQuill, false);

function loadQuill() {
    var script = document.createElement('script');
    script.src = "https://cdn.quilljs.com/1.0.0/quill.js";
    document.head.appendChild(script);
    script.onload = function () {

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
    var editor = document.getElementById("editor");
    if (editor === null) {
        return;
    }
    var quill = new Quill('#editor', {
        readOnly: true,
        theme: 'snow'
    });

    //Resume
    const resumeContent = document.getElementById("resume-content");
    const delta = quill.clipboard.convert(decodeURIComponent(resumeContent.innerHTML));
    quill.setContents(delta);
}