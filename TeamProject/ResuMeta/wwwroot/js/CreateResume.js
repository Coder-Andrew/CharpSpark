import {addEducationFromContainer, validateEducation} from './CreateResume_Modules/education-mod.js';
import {addProjectsFromContainer, validateProjects} from './CreateResume_Modules/project-mod.js';
import {addPersonalSummaryFromContainer, validatePersonalSummary} from './CreateResume_Modules/personalSummary-mod.js';
import {addEmploymentFromContainer, validateEmployment} from './CreateResume_Modules/employment-mod.js';
import {addAchievementsFromContainer, validateAchievements} from './CreateResume_Modules/achievement-mod.js';
import {validateDates} from './CreateResume_Modules/utility-mod.js';


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

    const closeModal1 = document.getElementById("close-modal1");
    closeModal1.addEventListener('click', () => closeModal(), false);
    const closeModal2 = document.getElementById("close-modal2");
    closeModal2.addEventListener('click', () => closeModal(), false);

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


    var modalButtons = Array.from(document.querySelectorAll('#open-modal'));
    modalButtons.forEach(button => {
        button.addEventListener('click', () => openModal(button, educationBox, employmentBox, achievementBox, projectBox), false);
    });
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
    
    // Ensure form has at least some information inputted
    if (educationList.length === 0 && employmentList.length === 0 && achievementList.length === 0 && projectsList.length === 0 && selectedSkills.length === 0) {
        validationArea.style.display = "block";
        validationMessage.innerHTML = "Please fill out at least one section";
        window.scrollTo(0, 0);
        return;
    }

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

    // Add delete event listener to education clone
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

function openModal(button, educationBox, employmentBox, achievementBox, projectBox) {
    console.log("Opening Modal...");
    const validationArea = document.getElementById("validationMessage");
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";
    const modalType = button.getAttribute("name");
    const userId = document.getElementById("userId").value;
    const modalLabel = document.getElementById("modal-label");
    modalLabel.textContent = "";
    const vmList = document.getElementById("vm-list");
    vmList.textContent = "";
    modalLabel.textContent = modalType;
    if (modalType === "Education") {
        const response = fetch(`/api/resume/education/${userId}`);
        try {
            response.then(res => res.json()).then(data => {
                if (data.length === 0) {
                    vmList.textContent = "No education entries found";
                    return;
                }
                data.forEach(education => {
                    const listItem = document.createElement("li");
                    const listItemText = document.createElement("div");
                    if (education.degree.minor === "N/A") {
                        listItemText.innerHTML = `<p>Institution: ${education.institution}</p><p>Summary: ${education.educationSummary}</p><p>Dates: ${education.startDate} to ${education.endDate}</p><p>Degree: ${education.degree.type} in ${education.degree.major}</p><hr />`;
                    } else {
                        listItemText.innerHTML = `<p>Institution: ${education.institution}</p><p>Summary: ${education.educationSummary}</p><p>Dates: ${education.startDate} to ${education.endDate}</p><p>Degree: ${education.degree.type} in ${education.degree.major} with a minor in ${education.degree.minor}</p><hr />`;
                    }
                    listItemText.onclick = () => {
                        addEducation(educationBox);
                        const educationElement = educationBox.lastElementChild;
                        educationElement.querySelector("#institutionName").value = education.institution;
                        educationElement.querySelector("#educationSummary").value = education.educationSummary;
                        educationElement.querySelector("#startDate").value = education.startDate;
                        educationElement.querySelector("#endDate").value = education.endDate;
                        if (education.completion == true)
                        {
                            educationElement.querySelector("#completed").value = "true";
                        }
                        else
                        {
                            educationElement.querySelector("#completed").value = "false";
                        }
                        educationElement.querySelector("#degreeType").value = education.degree.type;
                        educationElement.querySelector("#major").value = education.degree.major;
                        educationElement.querySelector("#minor").value = education.degree.minor;
                    }
                    listItem.appendChild(listItemText);
                    listItem.style.cursor = "pointer";
                    listItem.id = "modal-list-item";
                    vmList.appendChild(listItem);
                });
            });
        }
        catch {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Error fetching education";
            console.log("Error fetching education");
        }   
    }
    if (modalType === "Employment") {
        const response = fetch(`/api/resume/employment/${userId}`);
        try {
            response.then(res => res.json()).then(data => {
                if (data.length === 0) {
                    vmList.textContent = "No employment history found";
                    return;
                }
                data.forEach(employment => {
                    const listItem = document.createElement("li");
                    const listItemText = document.createElement("div");
                    listItemText.innerHTML = `<p>Company: ${employment.company}</p><p>Summary: ${employment.description}</p><p>Location: ${employment.location}</p><p>Job Title: ${employment.jobTitle}</p><p>Dates: ${employment.startDate} to ${employment.endDate}</p><p>Reference: ${employment.referenceContactInfo.firstName} ${employment.referenceContactInfo.lastName} ${employment.referenceContactInfo.phoneNumber}</p><hr />`;
                    listItemText.onclick = () => {
                        addEmployment(employmentBox);
                        const employmentElement = employmentBox.lastElementChild;
                        employmentElement.querySelector("#company").value = employment.company;
                        employmentElement.querySelector("#description").value = employment.description;
                        employmentElement.querySelector("#location").value = employment.location;
                        employmentElement.querySelector("#jobTitle").value = employment.jobTitle;
                        employmentElement.querySelector("#startDate").value = employment.startDate;
                        employmentElement.querySelector("#endDate").value = employment.endDate;
                        employmentElement.querySelector("#firstName").value = employment.referenceContactInfo.firstName;
                        employmentElement.querySelector("#lastName").value = employment.referenceContactInfo.lastName;
                        employmentElement.querySelector("#phoneNumber").value = employment.referenceContactInfo.phoneNumber;
                    }
                    listItem.appendChild(listItemText);
                    listItem.style.cursor = "pointer";
                    listItem.id = "modal-list-item";
                    vmList.appendChild(listItem);
                });
            });
        }
        catch {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Error fetching employment";
            console.log("Error fetching employment");
        }
    }
    if (modalType === "Achievements") {
        const response = fetch(`/api/resume/achievements/${userId}`);
        try {
            response.then(res => res.json()).then(data => {
                if (data.length === 0) {
                    vmList.textContent = "No achievements found";
                    return;
                }
                data.forEach(achievement => {
                    const listItem = document.createElement("li");
                    const listItemText = document.createElement("div");
                    listItemText.innerHTML = `<p>Title: ${achievement.title}</p><p>Summary: ${achievement.summary}</p><hr />`;
                    listItemText.onclick = () => {
                        addAchievement(achievementBox);
                        const achievementElement = achievementBox.lastElementChild;
                        achievementElement.querySelector("#ach-title").value = achievement.title;
                        achievementElement.querySelector("#ach-summary").value = achievement.summary;
                    }
                    listItem.appendChild(listItemText);
                    listItem.style.cursor = "pointer";
                    listItem.id = "modal-list-item";
                    vmList.appendChild(listItem);
                });
            });
        }
        catch {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Error fetching achievements";
            console.log("Error fetching achievements");
        }
    
    }
    if (modalType === "Projects") {
        const response = fetch(`/api/resume/projects/${userId}`);
        try {
            response.then(res => res.json()).then(data => {
                if (data.length === 0) {
                    vmList.textContent = "No projects found";
                    return;
                }
                data.forEach(project => {
                    const listItem = document.createElement("li");
                    const listItemText = document.createElement("div");
                    listItemText.innerHTML = `<p>Title: ${project.name}</p><p>Link: ${project.link}</p><p>Summary: ${project.summary}</p><hr />`;
                    listItemText.onclick = () => {
                        addProject(projectBox);
                        const projectElement = projectBox.lastElementChild;
                        projectElement.querySelector("#project-name").value = project.name;
                        projectElement.querySelector("#project-link").value = project.link;
                        projectElement.querySelector("#project-summary").value = project.summary;
                    }
                    listItem.appendChild(listItemText);
                    listItem.style.cursor = "pointer";
                    listItem.id = "modal-list-item";
                    vmList.appendChild(listItem);
                });
            });
        }
        catch {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Error fetching projects";
            console.log("Error fetching projects");
        }
    
    }
}

function closeModal() {
    const modalName = document.getElementById("modal-label");
    modalName.textContent = "";
    const vmList = document.getElementById("vm-list");
    vmList.textContent = "";
}