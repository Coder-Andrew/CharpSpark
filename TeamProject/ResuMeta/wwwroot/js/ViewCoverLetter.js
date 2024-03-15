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
    var quill = new Quill('#editor', {
        theme: 'snow',
        modules: {
            toolbar: toolbarOptions,
        }
    });
    var htmlContent = document.getElementById('cover-letter-container').outerHTML;
    const delta = quill.clipboard.convert(htmlContent);
    quill.setContents(delta);
    const saveBtn = document.getElementById("save-resume");
    saveBtn.addEventListener('click', () => getHtmlInfo(), false);
    const exportBtn = document.getElementById("export-pdf");
    exportBtn.addEventListener('click', () => exportPdf(quill), false);
}