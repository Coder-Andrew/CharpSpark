document.addEventListener("DOMContentLoaded", initializePage, false);

let selectedSkills = [];
let achievementList = [];
let achievementNum = 0;


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


    // get achievement box 
    const achievementBox = document.getElementById("achievement-box");

    // get and add event listener to add achievement button
    const addAchievementBtn = document.getElementById("achievement-add-btn");
    addAchievementBtn.addEventListener('click', () => addAchievement(achievementBox), false);

    // get and add event listener to clear achievements button
    const clearAchievementBtn = document.getElementById("achievement-clear-btn");
    clearAchievementBtn.addEventListener('click', () => clearAchievements(achievementBox), false);

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
    const educationForm = document.getElementById("educationForm");
    const validationArea = document.getElementById("validationMessage");
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";
    var textInputs = educationForm.getElementsByTagName('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;

    const achievementContainer = document.getElementById("achievement-box");
    if (validateAchievements(achievementContainer, validationArea, validationMessage)) return;


    for (var i = 0; i < textInputs.length; i++) {
        if (textInputs[i].id === "skills") continue;
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

    // This function will add all achievements to the achievementList array, this probably should have it's own validation
    // instead of relying on the validation above and the validateachievements function...
    addAchievementsFromContainer(); 

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
        }],
        skills: selectedSkills,
        achievements: achievementList
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

function clearAchievements(containerElement) {
    if (!confirm("Are you sure you want to clear all achievements?")) return;

    achievementList = [];
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

function addAchievementsFromContainer() {
    const achievementContainer = document.getElementById("achievement-box");

    Array.from(achievementContainer.children).forEach(child => {
        addAchievementToList(child);
    });
}