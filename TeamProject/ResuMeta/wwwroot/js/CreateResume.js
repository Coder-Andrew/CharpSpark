document.addEventListener("DOMContentLoaded", initializePage, false);

let selectedSkills = [];

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
        skills: selectedSkills
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

        selectedSkills = selectedSkills.filter(id => id !== skillId);
        skillCol.removeChild(skillPill);
    });

    if (!selectedSkills.includes(skillId)) {
        skillCol.appendChild(skillPill);
        selectedSkills.push({ 'skillId': skillId });
    }


     console.log(selectedSkills);
}