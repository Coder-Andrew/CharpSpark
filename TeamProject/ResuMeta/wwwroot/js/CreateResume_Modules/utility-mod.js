export function checkForIllegalCharacters(inputElement, validationElement, validationMessageElement) {
    // Might be better to move this so it finds from the document itself instead of being passed in
    // because it gets redundant to pass this many parameters in everytime...
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }
    const specialPattern = /[\\_\|\^%=+\(\)*\[\]\<\>\~\`]+/;

    if (inputElement.value.match(specialPattern)) {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = "Invalid character in form";
        window.scrollTo(0, 0);
        return true;
    }
    return false;
}

export function validateNonEmptyInput(inputElement, validationElement, validationMessageElement, message = "Please fill out all fields") {
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }

    if (inputElement.value === "") {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = message;
        window.scrollTo(0, 0);
        return true;
    }
    return false;
}

export function validateDates(startDateInputs, endDateInputs, validationElement, validationMessageElement) {
    return startDateInputs.some(function(startDateInput, index) {
        var endDateInput = endDateInputs[index];

        if(startDateInput && endDateInput) {
            var startDate = new Date(startDateInput.value);
            var endDate = new Date(endDateInput.value);

            if(startDate > endDate) {
                validationElement.style.display = "block";
                validationMessageElement.innerHTML = "Start date must be before end date";
                window.scrollTo(0, 0);
                return true;
            }
        }
        return false;
    });
}