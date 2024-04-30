import { checkForIllegalCharacters, validateNonEmptyInput } from "./CreateResume_Modules/utility-mod.js";

document.addEventListener("DOMContentLoaded", initializePage, false);
var coverLetterId = 1; 
var hiringManagerKnown = true;

function initializePage()
{
    document.getElementById('submitInfo').addEventListener('click', submitInfo);
    document.querySelector('.cover-letter-button').addEventListener('click', nameUnknown);
    document.querySelector('.name-unknown-button').addEventListener('click', nameFound);
}

async function submitInfo() {
    const validationArea = document.getElementById('validationMessage');
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";
    var hiringManager = "";

    //Validate fields
    if(hiringManagerKnown)
    {
        const hiringManagerContainer = document.getElementById('hiring-manager-box');
        if (validateHiringManager(hiringManagerContainer, validationArea, validationMessage)) return;
    }

    const coverLetterBodyContainer = document.getElementById('cover-letter-body-box');
    if (validateCoverLetterBody(coverLetterBodyContainer, validationArea, validationMessage)) return;


    if(hiringManagerKnown)
    {
        var title = document.querySelector('.cover-letter-drop-down').value;
        var lastName = document.querySelector('.cover-letter-address-inputs input').value;
        hiringManager = title + " " + lastName;
    }
    else
    {
        hiringManager = "Hiring Manager";
    }
    var coverLetterBody = document.querySelector('#cover-letter-body').value;

    const coverLetterInfo = {
        Id: document.getElementById('userId').value,
        HiringManager: hiringManager,
        Body: coverLetterBody
    };
    console.log(coverLetterInfo);
     const response = await fetch(`/api/coverletter/info`, {
         method: 'PUT',
         headers: {
             'Accept': 'application/json; application/problem+json; charset=utf-8',
             'Content-Type': 'application/json; charset=utf-8'
         },
         body: JSON.stringify(coverLetterInfo)
     });
     if(response.ok)
     {
         const responseJson = await response.json();
         const url = responseJson.redirectUrl;
         window.location.href = url;
     }
}

function nameUnknown() {
    document.querySelector('.cover-letter-address-form').style.display = 'none';
    document.querySelector('.cover-letter-address-form-name-unknown').style.display = 'block';
    hiringManagerKnown = false;
}

function nameFound() {
    document.querySelector('.cover-letter-address-form').style.display = 'flex';
    document.querySelector('.cover-letter-address-form-name-unknown').style.display = 'none';
    hiringManagerKnown = true;
}

function validateHiringManager(hiringManagerContainer, validationElement, validationMessageElement) {
    var hiringManagerInputs = hiringManagerContainer.querySelectorAll('input');
    var hiringManagerDropDown = hiringManagerContainer.querySelectorAll('select');

    for (var i = 0; i < hiringManagerInputs.length; i++) {
        if (checkForIllegalCharacters(hiringManagerInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(hiringManagerInputs[i], validationElement, validationMessageElement, "Please fill out all fields")) return true;
    }

    for (var i = 0; i < hiringManagerDropDown.length; i++) {
        if (validateNonEmptyInput(hiringManagerDropDown[i], validationElement, validationMessageElement, "Please fill out all fields")) return true;
    }
    return false;
}

function validateCoverLetterBody(coverLetterContainer, validationElement, validationMessageElement) {
    var coverLetterInputs = coverLetterContainer.querySelectorAll('input');
    var coverLetterTextArea = coverLetterContainer.querySelectorAll('textarea');

    for (var i = 0; i < coverLetterInputs.length; i++) {
        if (checkForIllegalCharacters(coverLetterInputs[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(coverLetterInputs[i], validationElement, validationMessageElement, "Please fill out all fields")) return true;
    }

    for (var i = 0; i < coverLetterTextArea.length; i++) {
        if (checkForIllegalCharacters(coverLetterTextArea[i], validationElement, validationMessageElement)) return true;
        if (validateNonEmptyInput(coverLetterTextArea[i], validationElement, validationMessageElement, "Please fill out all fields")) return true;
    }
    return false;
}