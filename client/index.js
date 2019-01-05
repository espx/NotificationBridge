const signalR = require("@aspnet/signalr");

function restart(e){
    console.log(e)
    console.log('restarting in 20s...')
    setTimeout(start, 20000)
}

function start() {
    console.log('starting...')
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:45661/notifications")
        .build()

    connection.on("Notify", data => {
        console.log(data);
    })
    connection.onclose(restart)
    connection.start()
        .then(() => connection.invoke("JoinGroup", "test2"))
        .catch(restart)

}

start()
