import { checkForIllegalCharacters, validateNonEmptyInput } from "./utility-mod.js";

export function addProjectsToList(projectElement, projectsList) {
    const projectName = projectElement.querySelector("#project-name").value;

    if (!projectName) return;
    const project = {
        "name": projectName,
        "summary": projectElement.querySelector("#project-summary").value,
        "link": projectElement.querySelector("#project-link").value
    }
    projectsList.push(project);
}

export function addProjectsFromContainer(projectContainer) {
    var projectsList = [];
    Array.from(projectContainer.children).forEach(child => {
        addProjectsToList(child, projectsList);
    });
    return projectsList;
}

export function validateProjects(projectContainer, validationElement, validationMessageElement) {
    const projectInputs = projectContainer.querySelectorAll("input");
    const projectTextAreas = projectContainer.querySelectorAll("textarea");


    for (var i = 0; i < projectInputs.length; i++) {
        if (checkForIllegalCharacters(projectInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(projectInputs[i], validationElement, validationMessageElement, "Please fill out all project names")) return true;
    }

    for (var i = 0; i < projectTextAreas.length; i++) {
        if (checkForIllegalCharacters(projectTextAreas[i], validationElement, validationMessageElement)) return true;
    }

    return false;
}
