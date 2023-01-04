var connection = new signalR.HubConnectionBuilder()
    .withUrl("/dumperHub")
    .configureLogging(signalR.LogLevel.Error)
    .build();
var dumperName = '';
document.addEventListener("DOMContentLoaded", function () {
    connection.start().then(function () {
        var pathName = window.location.pathname;
        var urlSections = pathName.split("/");
        dumperName = urlSections[urlSections.length - 1];
        try {
            connection.invoke("AddToGroup", dumperName, null);
        } catch (err) {
            console.error(err);
        }
    }).catch(function (err) { return console.error(err.toString()) });
});

window.onbeforeunload = function () {
    try {
        connection.invoke("RemoveFromGroup", dumperName, null);
    } catch (err) {
        console.error(err);
    }
}

connection.on("Notify", function (message) {
    var notifyElement = document.createElement("div");
    notifyElement.innerText = message;
    document.getElementById("notificationSection").appendChild(notifyElement);
    setTimeout(function () {
        notifyElement.remove();
    }, 3000);
});