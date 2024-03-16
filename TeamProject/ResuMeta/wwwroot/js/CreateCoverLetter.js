document.addEventListener("DOMContentLoaded", initializePage, false);
var coverLetterId = 1; 

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
    var title = document.querySelector('.cover-letter-drop-down').value;
    var lastName = document.querySelector('.cover-letter-address-inputs input').value;
    var hiringManager = title + " " + lastName;
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
}

function nameFound() {
    document.querySelector('.cover-letter-address-form').style.display = 'flex';
    document.querySelector('.cover-letter-address-form-name-unknown').style.display = 'none';
}