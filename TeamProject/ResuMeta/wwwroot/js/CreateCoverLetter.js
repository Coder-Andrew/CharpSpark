document.addEventListener("DOMContentLoaded", initializePage, false); 

function initializePage()
{
    document.getElementById('submitInfo').addEventListener('click', submitInfo);
    document.querySelector('.cover-letter-button').addEventListener('click', nameUnknown);
    document.querySelector('.name-unknown-button').addEventListener('click', nameFound);
}

async function submitInfo() {
    window.location.href = '/CoverLetter/ViewCoverLetter';
}

function nameUnknown() {
    document.querySelector('.cover-letter-address-form').style.display = 'none';
    document.querySelector('.cover-letter-address-form-name-unknown').style.display = 'block';
}

function nameFound() {
    document.querySelector('.cover-letter-address-form').style.display = 'flex';
    document.querySelector('.cover-letter-address-form-name-unknown').style.display = 'none';
}