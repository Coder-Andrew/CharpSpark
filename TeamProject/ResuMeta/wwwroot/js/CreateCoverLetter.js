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
    const validationArea = document.getElementById('validationMessage');
    validationArea.style.display = "none";
    const validationMessage = document.getElementById("validationText");
    validationMessage.innerHTML = "";
    var hiringManager = "";

    //Validate fields
    const coverLetterContainer = document.getElementById('cover-letter-box');
    if (validateCoverLetter(coverLetterContainer, validationArea, validationMessage)) return;


    if(hiringManagerKnown)
    {
        var title = document.querySelector('.cover-letter-drop-down').value;
        if(!title)
        {
            validationArea.style.display = "block";
            validationMessage.innerHTML = "Please select a title for the hiring manager";
            window.scrollTo(0, 0);
            return;
        }
        var lastName = document.querySelector('.cover-letter-address-inputs input').value;
        hiringManager = title + " " + lastName;
    }
    else
    {
        hiringManager = "Hiring Manager";
    }
    var coverLetterBody = document.querySelector('#cover-letter-body').value;

    const coverLetterInfo = {
        UserInfoId:  parseInt(document.getElementById('userId').value, 10),
        CoverLetterId: coverLetterId++,
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

function validateCoverLetter(coverLetterContainer, validationElement, validationMessageElement) {
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