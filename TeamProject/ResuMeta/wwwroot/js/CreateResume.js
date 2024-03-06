document.addEventListener("DOMContentLoaded", initializePage, false);

let selectedSkills = [];
let achievementList = [];
let projectsList = [];
let educationList = [];
let employmentList = [];
let achievementNum = 0;
let personalSummary = "";


function initializePage() {
    console.log("Create Resume Page loaded");
    const submitBtn = document.getElementById("submitInfo");
    submitBtn.addEventListener('click', () => submitInfo(), false);

    const skillsDropdown = document.getElementById("skills-dropdown");
    const skillInput = document.getElementById("skills");
    const skillCol = document.getElementById("skill-col");

    // Add debounce to event listener
    skillInput.addEventListener('input', debounce(() => {
        if (skillInput.value) {
            skillsDropdown.classList.add("show");
            getSkills(skillInput.value);
        } else {
            skillsDropdown.classList.remove("show");
        }

    }, 350), false);

    // Add event listener to dropdown
    skillsDropdown.addEventListener('click', (event) => {
        skillInput.value = "";
        skillsDropdown.classList.remove("show");
        addSkillToSkillList(event);
    }, false);


    // get info boxes
    const educationBox = document.getElementById("education-box");
    const employmentBox = document.getElementById("employment-box");
    const achievementBox = document.getElementById("achievement-box");
    const projectBox = document.getElementById("project-box");

    // get and add event listener to add info section buttons
    const addEducationBtn = document.getElementById("education-add-btn");
    addEducationBtn.addEventListener('click', () => addEducation(educationBox), false);
    const addEmploymentBtn = document.getElementById("employment-add-btn");
    addEmploymentBtn.addEventListener('click', () => addEmployment(employmentBox), false);
    const addAchievementBtn = document.getElementById("achievement-add-btn");
    addAchievementBtn.addEventListener('click', () => addAchievement(achievementBox), false);
    const addProjectBtn = document.getElementById("project-add-btn");
    addProjectBtn.addEventListener('click', () => addProject(projectBox), false);
    document.getElementById('personal-summary-add-btn').addEventListener('click', addPersonalSummary);

    // get and add event listener to clear info section buttons
    const clearEducationBtn = document.getElementById("education-clear-btn");
    clearEducationBtn.addEventListener('click', () => clearEducation(educationBox), false);
    const clearEmploymentBtn = document.getElementById("employment-clear-btn");
    clearEmploymentBtn.addEventListener('click', () => clearEmployment(employmentBox), false);
    const clearAchievementBtn = document.getElementById("achievement-clear-btn");
    clearAchievementBtn.addEventListener('click', () => clearAchievements(achievementBox), false);
    const clearProjectBtn = document.getElementById("project-clear-btn");
    clearProjectBtn.addEventListener('click', () => clearProjects(projectBox), false);

}

function debounce(func, delay) {
    let timeoutdId;
    return function () {
        const context = this;
        const args = arguments;
        clearTimeout(timeoutdId);
        timeoutdId = setTimeout(() => func.apply(context, args), delay);
    }
}

async function submitInfo() {
    const validationArea = document.getElementById("validationMessage");
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";

    const educationContainer = document.getElementById("education-box");
    if (educationContainer.children.length === 0) {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Please add at least one education entry";
        return;
    }
    if (validateEducation(educationContainer, validationArea, validationMessage))
    {
        educationList = [];
        return;
    }

    const employmentContainer = document.getElementById("employment-box");
    if (validateEmployment(employmentContainer, validationArea, validationMessage)) return;

    const achievementContainer = document.getElementById("achievement-box");
    if (validateAchievements(achievementContainer, validationArea, validationMessage)) return;

    const projectContainer = document.getElementById("project-box");
    if (validateProjects(projectContainer, validationArea, validationMessage)) return;

    const personalSummaryContainer = document.getElementById("personal-summary-box");
    if (validatePersonalSummary(personalSummaryContainer, validationArea, validationMessage)) return;

    // This function will add all achievements to the achievementList array, this probably should have it's own validation
    // instead of relying on the validation above and the validateachievements function...
    addEducationFromContainer();
    addEmploymentFromContainer();
    addAchievementsFromContainer(); 
    addProjectsFromContainer();
    addPersonalSummaryFromContainer();
   
    const resumeInfo = {
        id: userId.value,
        education: educationList,
        employment: employmentList,
        skills: selectedSkills,
        achievements: achievementList,
        projects: projectsList,
        personalSummary: personalSummary
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
        const responseJson = await response.json();
        const url = responseJson.redirectUrl
        console.log(url);
        window.location.href = url;
    }
    else
    {
        console.log("Error saving information");
    }
}

async function getSkills(subString) {
    // GET: api/resume/skills/{skillSubstring}
    const response = await fetch(`/api/resume/skills/${subString}`);

    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    const result = await response.json();
    //console.log(result);

    const skillsDropdown = document.getElementById('skills-dropdown');
    skillsDropdown.innerHTML = "";

    result.slice(0,10).forEach(skill => {
        const anchor = document.createElement('a');

        anchor.classList.add('dropdown-item');
        anchor.dataset.skillId = skill.id;
        anchor.textContent = skill.skillName;

        skillsDropdown.appendChild(anchor);
    })
}

function addSkillToSkillList(event) {
    if (event.target.tagName !== "A") {
        return;
    }
    //console.log(event.target);
    const skillCol = document.getElementById("skill-col");

    const skillPill = document.createElement("span");
    skillPill.classList.add('badge', 'rounded-pill', 'bg-orange');
    skillPill.textContent = event.target.textContent;

    const skillId = Number.parseInt(event.target.dataset.skillId);
    skillPill.dataset.skillId = skillId;

    // console.log(skillPill);

    // Add event listeners to skill badges
    skillPill.addEventListener('click', (event) => {
        if (event.target.tagName != "SPAN") return;

        selectedSkills = selectedSkills.filter(skill => skill.skillId !== skillId);
        console.log(selectedSkills);
        skillCol.removeChild(skillPill);
    });

    if (!selectedSkills.includes(skillId)) {
        skillCol.appendChild(skillPill);
        selectedSkills.push({ 'skillId': skillId });
    }


     console.log(selectedSkills);
}

function checkForIllegalCharacters(inputElement, validationElement, validationMessageElement) {
    // Might be better to move this so it finds from the document itself instead of being passed in
    // because it gets redundant to pass this many parameters in everytime...
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;

    if (inputElement.value.match(specialPattern)) {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = "Invalid character in form";
        return true;
    }
    return false;
}
function validateNonEmptyInput(inputElement, validationElement, validationMessageElement, message = "Please fill out all fields") {
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }

    if (inputElement.value === "") {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = message;
        return true;
    }
    return false;
}

function validateEducation(educationContainer, validationElement, validationMessageElement) {
    var textInputs = educationContainer.querySelectorAll('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const educationSummaries = document.querySelectorAll("#educationSummary");
    const completedList = document.querySelectorAll("#completed");
    const degreeTypeList = document.querySelectorAll("#degreeType");

    for (var i = 0; i < textInputs.length; i++) {
        if (textInputs[i].id === "skills") continue;
        if (textInputs[i].id === "minor" && textInputs[i].value === "") {
            textInputs[i].value = "N/A";
        }
        if (textInputs[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
        if (textInputs[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            return true;
        }
    }
    for (var i = 0; i < educationSummaries.length; i++) {
        if (educationSummaries[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
        if (educationSummaries[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            return true;
        }
    }
    for (var i = 0; i < completedList.length; i++) {
        if (completedList[i].value === "Select a value" )
        {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
    }
    for (var i = 0; i < degreeTypeList.length; i++) {
        if (degreeType.value === "Select a type" )
        {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
    }
    return false;
}  

function validateEmployment(employmentContainer, validationElement, validationMessageElement) {
    var textInputs = employmentContainer.querySelectorAll('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const employmentSummaries = document.querySelectorAll("#description");
    // const completedList = document.querySelectorAll("#completed");
    // const degreeTypeList = document.querySelectorAll("#degreeType");

    for (var i = 0; i < textInputs.length; i++) {
        if (textInputs[i].id === "skills") continue;
        if (textInputs[i].id === "minor" && textInputs[i].value === "") {
            textInputs[i].value = "N/A";
        }
        if (textInputs[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
        if (textInputs[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            return true;
        }
    }
    for (var i = 0; i < employmentSummaries.length; i++) {
        if (employmentSummaries[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            return true;
        }
        if (employmentSummaries[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            return true;
        }
    }
    // for (var i = 0; i < completedList.length; i++) {
    //     if (completedList[i].value === "Select a value" )
    //     {
    //         validationElement.style.display = "block";
    //         validationMessageElement.innerHTML = "Please fill out all fields";
    //         return true;
    //     }
    // }
    // for (var i = 0; i < degreeTypeList.length; i++) {
    //     if (degreeType.value === "Select a type" )
    //     {
    //         validationElement.style.display = "block";
    //         validationMessageElement.innerHTML = "Please fill out all fields";
    //         return true;
    //     }
    // }
    return false;
}    

function validateAchievements(achievementContainer, validationElement, validationMessageElement) {
    const achievementInputs = achievementContainer.querySelectorAll("input");
    const achievementTextAreas = achievementContainer.querySelectorAll("textarea");

    // ForEach loop does not respect return statements... so I had to use a regular for loop here
    for (var i = 0; i < achievementInputs.length; i++) {
        if (checkForIllegalCharacters(achievementInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(achievementInputs[i], validationElement, validationMessageElement, "Please fill out all achievement titles")) return true;
    }

    for (var i = 0; i < achievementTextAreas.length; i++) {
        if (checkForIllegalCharacters(achievementTextAreas[i], validationElement, validationMessageElement)) return true;
    }

    return false;
}

function validateProjects(projectContainer, validationElement, validationMessageElement) {
    const projectInputs = projectContainer.querySelectorAll("input");
    const projectTextAreas = projectContainer.querySelectorAll("textarea");


    for (var i = 0; i < projectInputs.length; i++) {
        if (checkForIllegalCharacters(projectInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(projectInputs[i], validationElement, validationMessageElement, "Please fill out all project names")) return true;
    }

    for (var i = 0; i < projectTextAreas.length; i++) {
        if (checkForIllegalCharacters(projectTextAreas[i], validationElement, validationMessageElement)) return true;
    }

    return false;
}

function validatePersonalSummary(personalSummaryContainer, validationElement, validationMessageElement) {
    var personalSummaryInputs = personalSummaryContainer.querySelectorAll('input');
    var personalSummaryTextArea = personalSummaryContainer.querySelectorAll('textarea');

    for (var i = 0; i < personalSummaryInputs.length; i++) {
        if (checkForIllegalCharacters(personalSummaryInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(personalSummaryInputs[i], validationElement, validationMessageElement, "Please fill out the personal summary or remove it")) return true;
    }

    for (var i = 0; i < personalSummaryTextArea.length; i++) {
        if (checkForIllegalCharacters(personalSummaryTextArea[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(personalSummaryTextArea[i], validationElement, validationMessageElement, "Please fill out the personal summary or remove it")) return true;
    }
    return false;
}

function addAchievement(containerElement) {
    const achievementTemplate = document.getElementById("achievement-template");
    const achievementClone = achievementTemplate.content.cloneNode(true);
    const cloneRoot = achievementClone.querySelector(".row");

    // Add delete event listenor to achievement clone
    achievementClone.querySelector(".btn").addEventListener("click", () => {        
        containerElement.removeChild(cloneRoot);
    }, false);

    containerElement.appendChild(achievementClone);
}

function addProject(containerElement) {
    const projectTemplate = document.getElementById("project-template");
    const projectClone = projectTemplate.content.cloneNode(true);
    const cloneRoot = projectClone.querySelector(".row");

    projectClone.querySelector(".btn").addEventListener("click", () => {        
        containerElement.removeChild(cloneRoot);
    }, false);

    containerElement.appendChild(projectClone);
} 

function addPersonalSummary() {
    const personalSummaryTemplate = document.getElementById("personal-summary-template");
    const personalSummaryClone = personalSummaryTemplate.content.cloneNode(true);
    const cloneRoot = personalSummaryClone.querySelector(".row");
    document.getElementById('personal-summary-btn').style.display = 'none';

    personalSummaryClone.querySelector(".btn").addEventListener("click", () => {
        document.getElementById("personal-summary-box").removeChild(cloneRoot);
        document.getElementById('personal-summary-btn').style.display = 'flex';
    }, false);

    document.getElementById("personal-summary-box").appendChild(personalSummaryClone);
}

function addEducation(containerElement) {
    const educationTemplate = document.getElementById("education-template");
    const educationClone = educationTemplate.content.cloneNode(true);
    const cloneRoot = educationClone.querySelector(".row");

    // Add delete event listenor to education clone
    educationClone.querySelector(".btn").addEventListener("click", () => {        
        containerElement.removeChild(cloneRoot);
    }, false);

    containerElement.appendChild(educationClone);

}

function addEmployment(containerElement) {
    const employmentTemplate = document.getElementById("employment-template");
    const employmentClone = employmentTemplate.content.cloneNode(true);
    const cloneRoot = employmentClone.querySelector(".row");

    // Add delete event listenor to employment clone
    employmentClone.querySelector(".btn").addEventListener("click", () => {        
        containerElement.removeChild(cloneRoot);
    }, false);

    containerElement.appendChild(employmentClone);

}

function clearAchievements(containerElement) {
    if (!confirm("Are you sure you want to clear all achievements?")) return;

    achievementList = [];
    containerElement.innerHTML = "";
}

function clearProjects(containerElement) {
    if (!confirm("Are you sure you want to clear all projects?")) return;

    projectsList = [];
    containerElement.innerHTML = "";
}

function clearEducation(containerElement) {
    if (!confirm("Are you sure you want to clear all education?")) return;

    educationList = [];
    containerElement.innerHTML = "";
}

function clearEmployment(containerElement) {
    if (!confirm("Are you sure you want to clear all employment?")) return;

    employmentList = [];
    containerElement.innerHTML = "";
}

function addAchievementToList(achievementElement) {
    const achievementTitle = achievementElement.querySelector("input").value;

    if (!achievementTitle) return;
    const achievement = {
        "title": achievementTitle,
        "body": achievementElement.querySelector("textarea").value
    }
    achievementList.push(achievement);
}

function addProjectsToList(projectElement) {
    const projectName = projectElement.querySelector("#project-name").value;

    if (!projectName) return;
    const project = {
        "name": projectName,
        "summary": projectElement.querySelector("#project-summary").value,
        "link": projectElement.querySelector("#project-link").value
    }
    projectsList.push(project);
}

function addEducationToList(educationElement) {
    const institution = educationElement.querySelector("#institutionName").value;
    const educationSummary = educationElement.querySelector("#educationSummary").value;
    const startDate = educationElement.querySelector("#startDate").value;
    const endDate = educationElement.querySelector("#endDate").value;
    const complete = educationElement.querySelector("#completed").value;
    const degreeType = educationElement.querySelector("#degreeType").value;
    const major = educationElement.querySelector("#major").value;
    const minor = educationElement.querySelector("#minor").value;

    if (!institution || !educationSummary || !startDate || !endDate || !complete || !degreeType || !major) return;
    const education = {
        "institution": institution,
        "educationSummary": educationSummary,
        "startDate": startDate,
        "endDate": endDate,
        "complete": complete,
        "degree": [{
            "type": degreeType,
            "major": major,
            "minor": minor
        }]
    }
    educationList.push(education);
}

function addEmploymentToList(employmentElement) {
    const company = employmentElement.querySelector("#company").value;
    const description = employmentElement.querySelector("#description").value;
    const location = employmentElement.querySelector("#location").value;
    const jobTitle = employmentElement.querySelector("#jobTitle").value;
    const startDate = employmentElement.querySelector("#startDate").value;
    const endDate = employmentElement.querySelector("#endDate").value;
    const firstName = employmentElement.querySelector("#firstName").value;
    const lastName = employmentElement.querySelector("#lastName").value;
    const phoneNumber = employmentElement.querySelector("#phoneNumber").value;

    if (!company || !description || !location || !jobTitle || !startDate || !endDate || !firstName || !lastName || !phoneNumber) return;
    const employmentHistory = {
        "company": company,
        "description": description,
        "location": location,
        "jobTitle": jobTitle,
        "startDate": startDate,
        "endDate": endDate,
        "referenceContactInfo": [{
            "firstName": firstName,
            "lastName": lastName,
            "phoneNumber": phoneNumber
        }]
    }
    employmentList.push(employmentHistory);
}

function addAchievementsFromContainer() {
    achievementList = [];
    const achievementContainer = document.getElementById("achievement-box");

    Array.from(achievementContainer.children).forEach(child => {
        addAchievementToList(child);
    });
}

function addProjectsFromContainer() {
    projectsList = [];
    const projectContainer = document.getElementById("project-box");

    Array.from(projectContainer.children).forEach(child => {
        addProjectsToList(child);
    });
}


function addEducationFromContainer() {
    educationList = [];
    const educationContainer = document.getElementById("education-box");

    Array.from(educationContainer.children).forEach(child => {
        addEducationToList(child);
    });
}

function addEmploymentFromContainer() {
    employmentList = [];
    const employmentContainer = document.getElementById("employment-box");

    Array.from(employmentContainer.children).forEach(child => {
        addEmploymentToList(child);
    });
}

function addPersonalSummaryFromContainer() {
    const personalSummaryContainer = document.getElementById("personal-summary-box");
    let textareaElement = personalSummaryContainer.querySelector("textarea");
    personalSummary = textareaElement ? textareaElement.value : "";
}