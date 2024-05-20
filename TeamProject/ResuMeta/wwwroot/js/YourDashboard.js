document.addEventListener('DOMContentLoaded', initializePage);

let jobLink = sessionStorage.getItem('jobLink');
let isImprovingResume = false;

function initializePage() {
    const dashBoardTitle = document.getElementById('dashboard-title');
    const resumeSection = document.getElementById('resume-section');
    var resumeSearchBar = document.getElementById('resume-search-bar');
    resumeSearchBar.addEventListener('keyup', function () {
        searchResume(resumeSearchBar);
    });
    var coverLetterSearchBar = document.getElementById('cover-letter-search-bar');
    coverLetterSearchBar.addEventListener('keyup', function () {
        searchCoverLetter(coverLetterSearchBar);
    });

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

function searchResume(searchBar) {
    var searchValue = searchBar.value.toLowerCase();
    var resumes = document.querySelectorAll('#resume-section .thumbnail');

    for (var i = 0; i < resumes.length; i++) {
        var title = resumes[i].querySelector('#resume-section .thumbnail-title').textContent.toLowerCase();
        if (title.includes(searchValue)) {
            resumes[i].style.display = '';
        } else {
            resumes[i].style.display = 'none';
        }
    }
}

function searchCoverLetter(searchBar) {
    var searchValue = searchBar.value.toLowerCase();
    var coverLetters = document.querySelectorAll('#cover-letter-section .thumbnail');

    for (var i = 0; i < coverLetters.length; i++) {
        var title = coverLetters[i].querySelector('#cover-letter-section .thumbnail-title').textContent.toLowerCase();
        if (title.includes(searchValue)) {
            coverLetters[i].style.display = '';
        } else {
            coverLetters[i].style.display = 'none';
        }
    }
}

