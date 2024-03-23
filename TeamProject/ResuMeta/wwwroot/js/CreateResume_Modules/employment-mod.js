

export function addEmploymentFromContainer(employmentContainer) {
    var employmentList = [];
    Array.from(employmentContainer.children).forEach(child => {
        addEmploymentToList(child, employmentList);
    });
    return employmentList;
}

export function addEmploymentToList(employmentElement, employmentList) {
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

export function validateEmployment(employmentContainer, validationElement, validationMessageElement) {
    var textInputs = employmentContainer.querySelectorAll('input');
    const specialPattern = /[\\_\|\^%=+\(\)#*\[\]\<\>\~\`]+/;
    const employmentSummaries = employmentContainer.querySelectorAll("#description");

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
    for (var i = 0; i < employmentSummaries.length; i++) {
        if (employmentSummaries[i].value === "") {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Please fill out all fields";
            window.scrollTo(0, 0);
            return true;
        }
        if (employmentSummaries[i].value.match(specialPattern)) {
            validationElement.style.display = "block";
            validationMessageElement.innerHTML = "Invalid character in form";
            window.scrollTo(0, 0);
            return true;
        }
    }
    return false;
}    