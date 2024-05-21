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
        if (resumeContent !== null) {
            const delta = quill.clipboard.convert(decodeURIComponent(resumeContent.innerHTML));
            quill.setContents(delta);
        }
        const upVoteBtn = document.getElementById("upvotes");
        upVoteBtn.addEventListener("click", upVote, false);
        const downVoteBtn = document.getElementById("downvotes");
        downVoteBtn.addEventListener("click", downVote, false);
    }
    
    isAlreadyFollowing();
    updateViewCount();
}

async function upVote() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/vote/${profileId}`;
    const vote = {
        "voteValue": "UP"
    }

    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(vote)
    });    
    if (response.ok)
    {
        refreshVotes();
    }
    else
    {
        console.log("Error voting");
    }
}

async function downVote() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/vote/${profileId}`;
    const vote = {
        "voteValue": "DOWN"
    }

    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        },
        body: JSON.stringify(vote)
    });    
    if (response.ok)
    {
        refreshVotes();
    }
    else
    {
        console.log("Error voting");
    }
}

async function refreshVotes() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/${profileId}`;
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
        document.getElementById("upvotes-count").textContent = data.upVoteCount;
        document.getElementById("downvotes-count").textContent = data.downVoteCount;
    }
    else
    {
        console.log("Error getting votes");
    }
}

async function updateViewCount() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/updateViews/${profileId}`;
    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (!response.ok)
    {
        console.log("Error updating view count");
    }
}

async function follow() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/follow/${profileId}`;
    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok)
    {
        const followBtn = document.getElementById("follow-btn");
        followBtn.textContent = "Unfollow";
        followBtn.removeEventListener("click", follow, false);
        followBtn.addEventListener("click", unfollow, false);
        refreshFollowers();
    }
    else
    {
        console.log("Error following user");
    }
}

async function unfollow() {
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/unfollow/${profileId}`;
    const response = await fetch(url, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (response.ok)
    {
        const followBtn = document.getElementById("follow-btn");
        followBtn.textContent = "Follow";
        followBtn.removeEventListener("click", unfollow, false);
        followBtn.addEventListener("click", follow, false);
        refreshFollowers();
    }
    else
    {
        console.log("Error unfollowing user");
    }

}

async function isAlreadyFollowing()
{
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/isFollowing/${profileId}`;
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
        if (data === 1)
        {
            const followBtn = document.getElementById("follow-btn");
            followBtn.disabled = true;
            followBtn.style = "display: none;";
        }
        if (data === true)
        {
            const followBtn = document.getElementById("follow-btn");
            followBtn.textContent = "Unfollow";
            followBtn.removeEventListener("click", follow, false);
            followBtn.addEventListener("click", unfollow, false);
        }
        else
        {
            const followBtn = document.getElementById("follow-btn");
            followBtn.textContent = "Follow";
            followBtn.removeEventListener("click", unfollow, false);
            followBtn.addEventListener("click", follow, false);
        }
    }
    else
    {
        console.log("Error checking if user is following");
        const followBtn = document.getElementById("follow-btn");
        followBtn.disabled = true;
        followBtn.style = "display: none;";
    }
}

async function refreshFollowers()
{
    const profileId = document.getElementById("profile-id").textContent;
    const url = `/api/profiles/${profileId}`;
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
        document.getElementById("follower-count").textContent = data.followerCount;
        document.getElementById("following-count").textContent = data.followingCount;
    }
    else
    {
        console.log("Error getting followers");
    }
}