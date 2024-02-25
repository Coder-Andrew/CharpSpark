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
        ['blockquote', 'code-block'],
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
    var htmlContent = document.getElementById('resume-container').outerHTML;
    const delta = quill.clipboard.convert(htmlContent);
    quill.setContents(delta);
    const saveBtn = document.getElementById("save-resume");
    saveBtn.addEventListener('click', () => getHtmlInfo(), false);
}

async function getHtmlInfo() 
{
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
            htmlContent: encodeURIComponent(htmlContent)
        })
    });    
    if (response.ok)
    {
        alert('Resume saved successfully.');
    }
    else
    {
        console.log("Error saving information");
        alert('An error occurred while saving the resume.');
    }
}