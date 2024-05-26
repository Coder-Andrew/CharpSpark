import { isPdfExtension } from "./ResumeIndex_Modules/utility-mod.js";

document.addEventListener("DOMContentLoaded", initializePage);


function initializePage() {
    console.log("Hello world!");

    document.getElementById('upload-btn').addEventListener('click', sendPdf);
    document.getElementById('help-btn').addEventListener('click', function() {
        document.getElementById('help-modal').style.display = "block";
    });
    
    document.getElementById('close-btn').addEventListener('click', function() {
        document.getElementById('help-modal').style.display = "none";
    });
}

function sendPdf() {
    const fileUpload = document.getElementById("pdf-upload");
    const file = fileUpload.files[0];

    if (file === undefined || !isPdfExtension(file.name)) return;

    let formData = new FormData();
    formData.append("pdfFile", file);

    let xhr = new XMLHttpRequest();
    xhr.open("POST", "api/importPdf", true);

    xhr.onload = function () {
        if (this.status == 200) {
            document.getElementById("success-msg").classList.remove("invisible");
            // Delay to allow the server to process the file
            setTimeout(redirectToDashboard, 1000);  
            console.log("Upload successful")
        } else {
            console.log("Upload failed");
        }
    }
    xhr.send(formData);
}

function redirectToDashboard() {
    window.location.href = "/Resume/YourDashboard";
}


