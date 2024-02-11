
document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    console.log("Create Resume Page loaded");
    const submitBtn = document.getElementById("submitInfo");
    submitBtn.addEventListener('click', () => submitInfo(), false);
}

async function submitInfo() {
    const educationForm = document.getElementById("educationForm");
    const userId = document.getElementById("userId");
    const institution = document.getElementById("institutionName");
    const educationSummary = document.getElementById("educationSummary");
    const startDate = document.getElementById("startDate");
    const endDate = document.getElementById("endDate");
    const completed = document.getElementById("completed");
    const degreeType = document.getElementById("degreeType");
    const major = document.getElementById("major");
    const minor = document.getElementById("minor");

    const resumeInfo = {
        id: userId.value,
        education: [{
            institution: institution.value,
            educationSummary: educationSummary.value,
            startDate: startDate.value,
            endDate: endDate.value,
            complete: completed.value,
            degree: [{
                type: degreeType.value,
                major: major.value,
                minor: minor.value
            }]
        }]
    };
    console.log(resumeInfo);

    const response = await fetch(`/api/resume/info`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(resumeInfo)
    });    
    if (response.ok)
    {
        console.log("Information Saved!");
    }
    else
    {
        console.log("Error saving information");
    }
}