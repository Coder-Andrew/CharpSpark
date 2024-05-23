document.addEventListener("DOMContentLoaded", mainInitializePage, false);

function mainInitializePage() {
    var consentModal = document.getElementById('cookie-consent-modal');
    var isAuthenticated = document.getElementById('is-authenticated').value === 'true';
    
    if (isAuthenticated || localStorage.getItem('cookiesAccepted') === 'true') {
        return;
    }

    consentModal.style.display = 'block';

    var declineModal = document.getElementById('decline-modal');
    var consentYes = document.getElementById('consent-yes');
    var consentNo = document.getElementById('consent-no');
    consentModal.style.display = 'block';
    consentYes.addEventListener('click', () => acceptCookie(consentModal), false);
    consentNo.addEventListener('click', () => declineCookie(declineModal, consentModal), false);
}

function acceptCookie(consentModal) {
    consentModal.style.display = 'none';
    localStorage.setItem('cookiesAccepted', 'true');
    console.log('Cookies accepted: ' + localStorage.getItem('cookiesAccepted'));
}

function declineCookie(declineModal, consentModal) {
    declineModal.style.display = 'block';
    consentModal.style.display = 'none';
}