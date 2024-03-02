document.addEventListener('DOMContentLoaded', initializePage, false);

function initializePage() {
    const showHideChat = document.getElementById('show-hide-chat');
    document.addEventListener('click', () => document.getElementById('chat-box').classList.toggle("invisible"), false);

    const chatTemplate = document.getElementById('chat-template');
    const node = generateChatNode(chatTemplate);
    chatGptNode(node);
    console.log(node);
}

function generateChatNode(template) {
    return document.importNode(template.content, true).children[0];
}

function chatGptNode(node) {
    node.classList.add("align-items-start")
    node.firstChild.classList.add("bg-primary");
}

function chatUserNode(node) {

}