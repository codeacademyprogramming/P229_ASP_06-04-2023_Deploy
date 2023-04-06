let connection = new signalR.HubConnectionBuilder().withUrl("/p229hub").build();

connection.start();

console.log(connection);

let sendButton = document.getElementById("sendButton");
let userInput = document.getElementById("userInput");
let messageInput = document.getElementById("messageInput");
let messagesList = document.getElementById("messagesList");
let groupContainer = document.getElementById("groupContainer");
let chatContainer = document.getElementById("chatContainer");
let group = document.getElementById("group");
let joinGroup = document.getElementById("joinGroup");

joinGroup.addEventListener('click', function (e) {
    e.preventDefault();

    let selectedGroup = group.value;
    console.log(selectedGroup);

    if (parseInt(selectedGroup) === 1 || parseInt(selectedGroup) === 2 || parseInt(selectedGroup) === 3) {
        //console.log('secilib')
        connection.invoke('JoinGroup', selectedGroup).then(error => {
            console.log(error)
        });

        groupContainer.classList.add('d-none');
        chatContainer.classList.remove('d-none');

    }
})

sendButton.addEventListener("click", function (e) {
    e.preventDefault();

    let user = userInput.value;
    let message = messageInput.value;
    let selectedGroup = group.value;
    connection.invoke("MesajGonder", user, message,selectedGroup);
})

connection.on("MesajQebulEden", function (user, messager)
{
    //let li = `<li>${user} - ${messager}</li>`
    let li = document.createElement('li')
    li.innerHTML = user + " - " + messager;

    messagesList.append(li);
})

connection.on("notify", function (message) {
    alert(message);

})