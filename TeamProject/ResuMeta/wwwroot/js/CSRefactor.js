import {addEducationFromContainer, validateEducation} from './cr_mods/education-mod.js';
import {addProjectsFromContainer, validateProjects} from './cr_mods/project-mod.js';
import {addPersonalSummaryFromContainer, validatePersonalSummary} from './cr_mods/personalSummary-mod.js';
import {addEmploymentFromContainer, validateEmployment} from './cr_mods/employment-mod.js';
import {addAchievementsFromContainer, validateAchievements} from './cr_mods/achievement-mod.js';
import {validateDates} from './cr_mods/utility-mod.js';


document.addEventListener("DOMContentLoaded", initializePage, false);

let selectedSkills = [];
let achievementList = [];
let projectsList = [];
let educationList = [];
let employmentList = [];
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

    // Add focus/unfocus event listener to skills
    skillInput.addEventListener('focus', () => {
        if (!skillsDropdown.firstElementChild | !skillInput.value) return;
        skillsDropdown.classList.add("show")
    });
    skillInput.addEventListener('blur', (event) => {
        console.log(event);
        setTimeout(() => {
            skillsDropdown.classList.remove("show")
        }, 100);
    });

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

    // Validate all containers
    const educationContainer = document.getElementById("education-box");
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

    // Validate all dates
    var startDateInputs = Array.from(document.querySelectorAll('#startDate'));
    var endDateInputs = Array.from(document.querySelectorAll('#endDate'));
    if (validateDates(startDateInputs, endDateInputs, validationArea, validationMessage)) return;

    // This function will add all achievements to the achievementList array, this probably should have it's own validation
    // instead of relying on the validation above and the validateachievements function...
    educationList = addEducationFromContainer(educationContainer);
    employmentList = addEmploymentFromContainer(employmentContainer);
    achievementList = addAchievementsFromContainer(achievementContainer); 
    projectsList = addProjectsFromContainer(projectContainer);
    personalSummary = addPersonalSummaryFromContainer(personalSummaryContainer);
   
    // Set and Send info
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

    result.slice(0, 10).forEach(skill => {
        if (selectedSkills.some(s => s.skillId == skill.id)) return;
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

    if (!selectedSkills.some(s => s.skillId == skillId)) {
        skillCol.appendChild(skillPill);
        selectedSkills.push({ 'skillId': skillId });
    }


     console.log(selectedSkills);
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
    // Add delete event listener to employment clone
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