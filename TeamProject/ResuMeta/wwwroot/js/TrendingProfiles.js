document.addEventListener("DOMContentLoaded", initializePage);

function initializePage() {
    updateTrending();
    getTrendingProfiles();
}

async function updateTrending() {
    const url = `/api/profiles/UpdateTrendingProfiles`;
    const response = await fetch(url, {
        method: 'POST',
    });
}

async function getTrendingProfiles() {
    const profileArea = document.getElementById("profile-area");
    profileArea.textContent = "";

    const url = `/api/profiles/GetTrendingProfiles`;
    const response = await fetch(url, {
        method: 'GET',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
        }
    });

    if (!response.ok) {
        profileArea.textContent = "Error loading profiles";
        return;
    }

    const profiles = await response.json();

    console.log(profiles);

    profiles.forEach(profile => {
        const node = cloneNode(profile);
        profileArea.appendChild(node);
    })
}

function cloneNode(jsonProfile) {
    const template = document.getElementById("profile-card-template");
    const firstElement = template.content.cloneNode(true).firstElementChild;

    const profileId          =  firstElement.querySelector(".profile-id");
    const profileImage       =  firstElement.querySelector(".profile-image");
    const profileLink        =  firstElement.querySelector(".profile-link");
    const profileViews       =  firstElement.querySelector(".profile-views");
    const profileScore       =  firstElement.querySelector(".profile-score");
    const profileUpvotes     =  firstElement.querySelector(".profile-upvotes");
    const profileDownvotes   =  firstElement.querySelector(".profile-downvotes");
    const profileName        =  firstElement.querySelector(".profile-name");
    const profileDescription =  firstElement.querySelector(".profile-description");


    profileLink.href               = `/Profile/UserProfile/${jsonProfile.profileId}`;
    profileId.value                 = jsonProfile.profileId;
    profileImage.src                = jsonProfile.profilePicturePath ? `data:image;base64,${jsonProfile.profilePicturePath}` : "/Images/profile.svg";
    profileViews.textContent        = jsonProfile.viewCount;
    profileScore.textContent        = jsonProfile.profileScore;
    profileUpvotes.textContent      = jsonProfile.upVoteCount;
    profileDownvotes.textContent    = jsonProfile.downVoteCount;
    profileName.textContent         = `${jsonProfile.firstName} ${jsonProfile.lastName}`;
    profileDescription.textContent  = jsonProfile.description.length > 100 ? jsonProfile.description.substring(0, 100) + "..." : jsonProfile.description;



    return firstElement;
}