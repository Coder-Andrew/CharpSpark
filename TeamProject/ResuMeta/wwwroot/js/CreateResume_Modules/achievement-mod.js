import { checkForIllegalCharacters, validateNonEmptyInput } from "./utility-mod.js";

export function validateAchievements(achievementContainer, validationElement, validationMessageElement) {
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

export function addAchievementToList(achievementElement, achievementList) {
    const achievementTitle = achievementElement.querySelector("input").value;

    if (!achievementTitle) return;
    const achievement = {
        "title": achievementTitle,
        "body": achievementElement.querySelector("textarea").value
    }
    achievementList.push(achievement);
}

export function addAchievementsFromContainer(achievementContainer) {
    var achievementList = [];
    Array.from(achievementContainer.children).forEach(child => {
        addAchievementToList(child, achievementList);
    });
    return achievementList;
}