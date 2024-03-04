document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    if (!document.getElementById('chat-body')) return;

    setupChatToggle();
    setupMessageInput();
    setupSendButton();
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
