
export function validateEducation(educationContainer, validationElement, validationMessageElement) {
    var textInputs = educationContainer.querySelectorAll('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const educationSummaries = educationContainer.querySelectorAll("#educationSummary");
    const completedList = educationContainer.querySelectorAll("#completed");
    const degreeTypeList = educationContainer.querySelectorAll("#degreeType");

    for (var i = 0; i < textInputs.length; i++) {
        if (textInputs[i].id === "skills") continue;
        if (textInputs[i].id === "minor" && textInputs[i].value === "") {
            textInputs[i].value = "N/A";
        }
        if (textInputs[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            window.scrollTo(0, 0);
            return true;
        }
        if (textInputs[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            window.scrollTo(0, 0);
            return true;
        }
    }
    for (var i = 0; i < educationSummaries.length; i++) {
        if (educationSummaries[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            window.scrollTo(0, 0);
            return true;
        }
        if (educationSummaries[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            window.scrollTo(0, 0);
            return true;
        }
    }
    for (var i = 0; i < completedList.length; i++) {
        if (completedList[i].value === "Select a value") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            window.scrollTo(0, 0);
            return true;
        }
    }
    for (var i = 0; i < degreeTypeList.length; i++) {
        if (degreeType.value === "Select a type") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            window.scrollTo(0, 0);
            return true;
        }
    }
    return false;
}

export function addEducationToList(educationElement, educationList) {
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

export function addEducationFromContainer(educationContainer) {
    var educationList = [];
    Array.from(educationContainer.children).forEach(child => {
        addEducationToList(child, educationList);
    });
    return educationList;
}
