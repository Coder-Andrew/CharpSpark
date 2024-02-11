
document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    console.log("Create Resume Page loaded");
    const submitBtn = document.getElementById("submitInfo");
    submitBtn.addEventListener('click', () => submitInfo(), false);
}

async function submitInfo() {
    const educationForm = document.getElementById("educationForm");
    const validationArea = document.getElementById("validationMessage");
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";
    var textInputs = educationForm.getElementsByTagName('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    for (var i = 0; i < textInputs.length; i++) {
        if (textInputs[i].id === "minor" && textInputs[i].value === "") {
            textInputs[i].value = "N/A";
        }
        if (textInputs[i].value === "") {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Please fill out all fields";
            return;
        }
        if (textInputs[i].value.match(specialPattern)) {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Invalid character in form";
            return;
        }
    }
    const educationSummary = document.getElementById("educationSummary");
    if (educationSummary.value === "") {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Please fill out all fields";
        return;
    }
    if (educationSummary.value.match(specialPattern)) {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Invalid character in form";
        return;
    }
    const userId = document.getElementById("userId");
    const institution = document.getElementById("institutionName");
    const startDate = document.getElementById("startDate");
    const endDate = document.getElementById("endDate");
    const completed = document.getElementById("completed");
    if (completed.value === "Select a value" )
    {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Please fill out all fields";
        return;
    }
    const degreeType = document.getElementById("degreeType");
    if (degreeType.value === "Select a type" )
    {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Please fill out all fields";
        return;
    }
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
        window.location.href = "/Resume/ViewResume";
    }
    else
    {
        console.log("Error saving information");
    }
}