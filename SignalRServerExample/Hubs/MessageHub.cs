using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRServerExample.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessageAsync(string message)
        {
            Clients.All.SendAsync("receiveMessage",message);
        }
    }
}
