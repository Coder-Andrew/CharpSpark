import { checkForIllegalCharacters, validateNonEmptyInput } from "./utility-mod.js";

export function addPersonalSummaryFromContainer(personalSummaryContainer) {
    let textareaElement = personalSummaryContainer.querySelector("textarea");
    let personalSummary = textareaElement ? textareaElement.value : "";
    return personalSummary;
}

export function validatePersonalSummary(personalSummaryContainer, validationElement, validationMessageElement) {
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