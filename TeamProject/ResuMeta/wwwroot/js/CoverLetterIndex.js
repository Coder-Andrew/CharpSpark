document.addEventListener("DOMContentLoaded", initializePage);


function initializePage() {
    document.getElementById('help-btn').addEventListener('click', function() {
        document.getElementById('help-modal').style.display = "block";
    });
    
    document.getElementById('close-btn').addEventListener('click', function() {
        document.getElementById('help-modal').style.display = "none";
    });
}