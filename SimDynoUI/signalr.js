const signalR = require('@microsoft/signalr');

const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/simdynohub") // Replace with your server's URL
    .withAutomaticReconnect()
    .build();

// Start the SignalR connection
async function startConnection() {
    try {
        await connection.start();
        console.log("Connected to server");
    } catch (err) {
        console.error("Connection failed:", err);
        setTimeout(startConnection, 5000); // Retry connection every 5 seconds
    }
}

// Register event handlers
function registerHandlers(handlers) {
    if (handlers.ReceiveData) {
        connection.on("ReceiveData", handlers.ReceiveData);
    }
    if (handlers.ClientConnected) {
        connection.on("ClientConnected", handlers.ClientConnected);
    }
    if (handlers.ClientDisconnected) {
        connection.on("ClientDisconnected", handlers.ClientDisconnected);
    }
    if (handlers.BroadcastMessage) {
        connection.on("BroadcastMessage", handlers.BroadcastMessage);
    }

    connection.onreconnected(() => {
        console.log("Reconnected to server.");
        if (handlers.onReconnect) handlers.onReconnect();
    });

    connection.onclose(() => {
        console.log("Disconnected from server.");
        if (handlers.onClose) handlers.onClose();
        startConnection(); // Automatically try to reconnect
    });
}

// Export the connection and functions
module.exports = {
    connection,
    startConnection,
    registerHandlers,
};