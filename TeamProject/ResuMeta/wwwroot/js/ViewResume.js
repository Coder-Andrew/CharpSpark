document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    const saveBtn = document.getElementById("save-resume");
    saveBtn.addEventListener('click', () => getHtmlInfo(), false);
}

async function getHtmlInfo() 
{
    console.log("Save Resume button clicked");
    const resumeId = document.getElementById('resume-id').value;
    var htmlContent = document.getElementById('resume-container').outerHTML;
    console.log(htmlContent);
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