document.addEventListener('DOMContentLoaded', initializePage);

const jobLink = sessionStorage.getItem('jobLink');

function initializePage() {
    const dashBoardTitle = document.getElementById('dashboard-title');
    const resumeSection = document.getElementById('resume-section');

    console.log(resumeSection);

    resumeSection.addEventListener('click', function (event) {
        let target = event.target;
        console.log('test');

        if (target.closest('.thumbnail')) {
            event.preventDefault();
            event.stopPropagation();
           
        }




    })
    //console.log(jobLink);
    if (jobLink) {
        dashBoardTitle.textContent = "Select a resume you'd like to improve";
    }

    // Clear storage when user navigates away from dashboard page after 
    // selecting a job description
    window.addEventListener('beforeunload', function (event) {
        sessionStorage.clear();
    });


}

