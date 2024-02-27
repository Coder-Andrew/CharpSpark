document.addEventListener('DOMContentLoaded', displayResumes);

async function displayResumes() {
    //Fix this to fetch current user's id
    var userId = 1;
    const url = `/api/resume/${userId}`;
    const response = await fetch(url);
    
    if(response.ok)
    {
        var data = await response.json();
        var section = document.getElementById('resume-section');
        
        data.forEach(resume => {
            var resumeDiv = document.createElement('div');
            resumeDiv.className = 'thumbnail';
            resumeDiv.innerHTML = decodeURIComponent(resume.htmlContent);
            var resumeTitle = document.createElement('div');
            resumeTitle.className = 'thumbnail-title';
            resumeTitle.innerText = resume.title;
            resumeDiv.appendChild(resumeTitle);
            resumeDiv.onclick = function() {
                window.location.href = '/Resume/YourResume/' + resume.resumeId;
            };
            section.appendChild(resumeDiv);
        });
    
    }
    else
    {
        const errorMessage = await response.text();
        div.innerText = `Error: ${errorMessage}`;
    }
}