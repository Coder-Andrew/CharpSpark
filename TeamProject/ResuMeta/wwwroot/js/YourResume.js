document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    const resumeContent = document.getElementById("resume-content").value;
    const resumeArea = document.getElementById("resume-area");
    resumeArea.innerHTML = decodeURIComponent(resumeContent);
}