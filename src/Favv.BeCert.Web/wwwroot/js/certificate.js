"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/certificateHub").build();


connection.on("ReceiveUpdate", function (update) {
    var msg = update.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var li = document.createElement("li");
    li.textContent = msg;
    document.getElementById("updateList").appendChild(li);
});

connection.start().then(function () {
    connection.invoke("GetUpdateAsync").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
}).catch(function (err) {
    return console.error(err.toString());
});

