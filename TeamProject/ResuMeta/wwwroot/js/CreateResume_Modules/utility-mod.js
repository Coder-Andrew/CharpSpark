export function checkForIllegalCharacters(inputElement, validationElement, validationMessageElement) {
    // Might be better to move this so it finds from the document itself instead of being passed in
    // because it gets redundant to pass this many parameters in everytime...
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }

    if (containsIllegalCharacter(inputElement.value)) {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = "Invalid character in form";
        window.scrollTo(0, 0);
        return true;
    }
    return false;
}

export function containsIllegalCharacter(inputValue) {
    return /[\\_\|\^%=\(\)*\[\]\<\>\~\`;]+/.test(inputValue);
}

export function validateNonEmptyInput(inputElement, validationElement, validationMessageElement, message = "Please fill out all fields") {
    if (!inputElement | !validationElement | !validationMessageElement) {
        throw new Error("Elements cannot be null");
    }

    if (containsEmptyInput(inputElement.value)) {
        validationElement.style.display = "block";
        validationMessageElement.innerHTML = message;
        window.scrollTo(0, 0);
        return true;
    }
    return false;
}

export function containsEmptyInput(inputValue) {
    return inputValue === "";
}

export function validateDates(startDateInputs, endDateInputs, validationElement, validationMessageElement) {
    return startDateInputs.some(function(startDateInput, index) {
        var endDateInput = endDateInputs[index];

        if(startDateInput && endDateInput) {
            if(containsInvalidDate(startDateInput.value, endDateInput.value)) {
                validationElement.style.display = "block";
                validationMessageElement.innerHTML = "Start date must be before end date";
                window.scrollTo(0, 0);
                return true;
            }
        }
        return false;
    });
}

export function containsInvalidDate(startDate, endDate) {
    return new Date(startDate) > new Date(endDate);
}