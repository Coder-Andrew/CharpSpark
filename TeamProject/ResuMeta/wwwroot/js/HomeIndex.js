document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    var searchBar = document.getElementById('search-profiles');
    searchBar.addEventListener('keyup', debounce(searchProfiles, 500), false);
    if (!document.getElementById('chat-body')) return;

    setupChatToggle();
    setupMessageInput();
    setupSendButton();
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

async function searchProfiles() {
    const profileContainer = document.getElementById('profile-container');
    profileContainer.innerHTML = "";
    const searchValue = document.getElementById('search-profiles').value;
    if (searchValue.length < 3) return;
    const response = await fetch(`/api/profiles/search/${searchValue}`, {
        method: 'GET',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=utf-8'
        }
    });
    if (!response.ok){
        profileContainer.innerHTML = "No profiles found";
        return;
    }
    const data = await response.json();

    data.forEach(profile => {
        const profileNode = generateProfileNode(profile);
        profileContainer.appendChild(profileNode);
    });
}

function generateProfileNode(profile) {
    const profileNode = document.getElementById('profile-template').content.cloneNode(true).firstElementChild;
    profileNode.querySelector('#profile-username').textContent = profile.userName;
    profileNode.querySelector('#profile-name').textContent = profile.firstName + " " + profile.lastName;
    profileNode.addEventListener('click', () => window.location.href = `/Profile/UserProfile/${profile.profileId}`, false);
    return profileNode;
}


function setupChatToggle() {
    const showHideChat = document.getElementById('show-hide-chat');
    showHideChat.addEventListener('click', toggleChatVisibility, false);
}

function toggleChatVisibility() {
    document.getElementById('chat-body').classList.toggle("invisible");
}

function setupMessageInput() {
    const messageInput = document.getElementById("message-input");
    messageInput.addEventListener('keyup', function (event) {
        if (event.key === 'Enter' || event.keyCode === 13) {
            sendChat(messageInput.value);
        }
    });
}

function setupSendButton() {
    document.getElementById("send").addEventListener('click', () => sendChat(document.getElementById("message-input").value), false);
}

function generateChatNode(templateId) {
    const template = document.getElementById(templateId);
    return template.content.cloneNode(true).firstElementChild;
}

function addClassesToChatNode(node, message, alignment, bgClass) {
    node.classList.add(alignment);
    node.firstElementChild.classList.add(bgClass);
    node.firstElementChild.textContent = message;
}

function addNodeToContainer(message, alignment, bgClass, containerId) {
    const container = document.getElementById(containerId);
    const node = generateChatNode("chat-template");
    addClassesToChatNode(node, message, alignment, bgClass);
    container.appendChild(node);

    container.scrollTop = container.scrollHeight;
}

async function sendChat(message) {
    const textBox = document.getElementById('message-input');
    if (!textBox || !textBox.value) return;

    addNodeToContainer(textBox.value, "align-items-end", "bg-success", 'chat-box');

    textBox.value = "Please wait, generating response...";

    const response = await fetch(`/api/cgpt/ask`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-type': 'application/json; charset=UTF-8'
        },
        body: JSON.stringify({ "question": message })
    });
    const data = await response.json();
    textBox.value = "";

    addNodeToContainer(data["response"], "align-items-start", "bg-primary", 'chat-box');
}
