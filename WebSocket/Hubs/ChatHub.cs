using Microsoft.AspNetCore.SignalR;

namespace WebSocket.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            if (user != "" && message != "" )
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
          

        }
    }
}
