document.addEventListener('DOMContentLoaded', initializePage);

let jobLink = sessionStorage.getItem('jobLink');
let isImprovingResume = false;

function initializePage() {
    const dashBoardTitle = document.getElementById('dashboard-title');
    const resumeSection = document.getElementById('resume-section');

    console.log(resumeSection);

    // Moved onclick in html to js eventlistener
    resumeSection.addEventListener('click', function (event) {
        let target = event.target.closest('.thumbnail');

        // If the user is selecting a resume to improve, store the resumeId in session storage
        if (target && jobLink) {
            isImprovingResume = true;
            const resumeId = target.dataset.url.split('/').pop();
            sessionStorage.setItem('resumeId', resumeId); // Get the resumeId from the url
            window.location.href = `/Resume/ImproveResume/${resumeId}`;
        } else if (target) {
            window.location.href = target.dataset.url;
        }
    })


    // Change dashboard title to indicate that user is selecting a resume to improve
    if (jobLink) {
        dashBoardTitle.textContent = "Select a resume you'd like to improve";
    }

    // Clear storage when user navigates away from dashboard page after 
    // selecting a job description
    window.addEventListener('beforeunload', function (event) {
        if (!isImprovingResume) {
            sessionStorage.clear(); // might want to only clear jobLink from session storage
        }
    });


}

