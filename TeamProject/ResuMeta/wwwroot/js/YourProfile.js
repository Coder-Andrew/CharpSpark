document.addEventListener("DOMContentLoaded", loadQuill, false);

function loadQuill() {
    var script = document.createElement('script');
    script.src = "https://cdn.quilljs.com/1.0.0/quill.js";
    document.head.appendChild(script);
    script.onload = function () {

        //Register the DividerBlot
        const BlockEmbed = Quill.import('blots/block/embed');

        class DividerBlot extends BlockEmbed {
        static create() {
            let node = super.create();
            return node;
        }

        static formats() {
            return true;
        }
        }

        DividerBlot.blotName = 'divider';
        DividerBlot.tagName = 'hr';

        Quill.register(DividerBlot);
        initializePage();
    }
}

function initializePage() {
    var editor = document.getElementById("editor");
    if (editor !== null) {
        var quill = new Quill('#editor', {
            readOnly: true,
            theme: 'snow'
        });

        //Resume
        const resumeContent = document.getElementById("resume-content");
        const delta = quill.clipboard.convert(decodeURIComponent(resumeContent.innerHTML));
        quill.setContents(delta);
    }
    const modalCloseBtn1 = document.getElementById("close-modal1");
    const modalCloseBtn2 = document.getElementById("close-modal2");
    modalCloseBtn1.addEventListener('click', closeModal, false);
    modalCloseBtn2.addEventListener('click', closeModal, false);
    const followers = document.getElementById("followers");
    followers.addEventListener('click', function() {openModal("Followers")}, false);
    const following = document.getElementById("following");
    following.addEventListener("click", function() {openModal("Following")}, false);
}

async function openModal(title) {
    closeModal();
    const modalBody = document.getElementById("modal-body");
    const modalLable = document.getElementById("modal-label");
    if (title === "Followers")
    {
        const profileId = document.getElementById("profile-id").textContent;
        const url = `/api/profiles/followers/${profileId}`;
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json; application/problem+json; charset=utf-8',
                'Content-Type': 'application/json; charset=utf-8'
            }
        });
        if (response.ok)
        {
            const data = await response.json();
            if (data.length === 0)
                {
                    const followingDiv = document.createElement("div");
                    followingDiv.className = "follower";
                    followingDiv.textContent = "No followers found";
                    modalBody.appendChild(followingDiv);
                }
            data.forEach(follower => {
                const profileTemplate = document.getElementById("profile-template");
                const templateClone = profileTemplate.content.cloneNode(true);
                const clone = templateClone.querySelector(".card-body");
                const cloneLink = clone.querySelector("#profile-link");
                const cloneUsername = clone.querySelector("#profile-username");
                const cloneFirstName = clone.querySelector("#profile-first-name");
                const cloneLastName = clone.querySelector("#profile-last-name");
                const cloneProfilePic = clone.querySelector("#profile-pic");
                if (follower.profilePicturePath !== null && follower.profilePicturePath.length > 0)
                {
                    const profilePic = document.createElement("img");
                    profilePic.src = `data:image;base64,${follower.profilePicturePath}`;
                    profilePic.alt = "Profile Picture";
                    profilePic.className = "img-thumbnail";
                    profilePic.style = "width: 50px; height: 50px;";
                    profilePic.id = "currentProfilePicture";
                    cloneProfilePic.appendChild(profilePic);
                }
                else
                {
                    const profilePic = document.createElement("img");
                    profilePic.src = "/Images/profile.svg";
                    profilePic.alt = "Profile Picture";
                    profilePic.className = "profile-placeholder img-thumbnail";
                    profilePic.style = "width: 50px; height: 50px;";
                    profilePic.id = "currentProfilePicture";
                    cloneProfilePic.appendChild(profilePic);
                }
                cloneUsername.textContent = follower.userName;
                cloneFirstName.textContent = follower.firstName;
                cloneLastName.textContent = follower.lastName;
                cloneLink.href = `/Profile/UserProfile/${follower.profileId}`;
                modalBody.appendChild(clone);
            });
        }
        else
        {
            const followerDiv = document.createElement("div");
            followerDiv.className = "follower";
            followerDiv.textContent = "No followers found";
            console.log("Error getting followers");
        }
    }
    if (title === "Following")
    {
        const profileId = document.getElementById("profile-id").textContent;
        const url = `/api/profiles/following/${profileId}`;
        const response = await fetch(url, {
            method: 'GET',
            headers: {
                'Accept': 'application/json; application/problem+json; charset=utf-8',
                'Content-Type': 'application/json; charset=utf-8'
            }
        });
        if (response.ok)
        {
            const data = await response.json();
            if (data.length === 0)
            {
                const followingDiv = document.createElement("div");
                followingDiv.className = "follower";
                followingDiv.textContent = "Not following anyone";
                modalBody.appendChild(followingDiv);
            }
            data.forEach(following => {
                const profileTemplate = document.getElementById("profile-template");
                const templateClone = profileTemplate.content.cloneNode(true);
                const clone = templateClone.querySelector(".card-body");
                const cloneLink = clone.querySelector("#profile-link");
                const cloneUsername = clone.querySelector("#profile-username");
                const cloneFirstName = clone.querySelector("#profile-first-name");
                const cloneLastName = clone.querySelector("#profile-last-name");
                const cloneProfilePic = clone.querySelector("#profile-pic");
                if (following.profilePicturePath !== null && following.profilePicturePath.length > 0)
                {
                    const profilePic = document.createElement("img");
                    profilePic.src = `data:image;base64,${following.profilePicturePath}`;
                    profilePic.alt = "Profile Picture";
                    profilePic.className = "img-thumbnail";
                    profilePic.style = "width: 50px; height: 50px;";
                    profilePic.id = "currentProfilePicture";
                    cloneProfilePic.appendChild(profilePic);
                }
                else
                {
                    const profilePic = document.createElement("img");
                    profilePic.src = "/Images/profile.svg";
                    profilePic.alt = "Profile Picture";
                    profilePic.className = "profile-placeholder img-thumbnail";
                    profilePic.style = "width: 50px; height: 50px;";
                    profilePic.id = "currentProfilePicture";
                    cloneProfilePic.appendChild(profilePic);
                }
                cloneUsername.textContent = following.userName;
                cloneFirstName.textContent = following.firstName;
                cloneLastName.textContent = following.lastName;
                cloneLink.href = `/Profile/UserProfile/${following.profileId}`;
                modalBody.appendChild(clone);
            });
        }
        else
        {
            const followingDiv = document.createElement("div");
            followingDiv.className = "follower";
            followingDiv.textContent = "Not following anyone";
            console.log("Error getting following");
        }
    }
    modalLable.textContent = title;
}

function closeModal() {
    const modalBody = document.getElementById("modal-body");
    const modalLable = document.getElementById("modal-label");

    modalLable.textContent = "";
    modalBody.textContent = "";

}