using Microsoft.AspNetCore.SignalR;

namespace SignalRService
{
    public class ServiceHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Connected", "connected");         
            await base.OnConnectedAsync();
        }

        public string GetConnectionId()
        {
            return Context.ConnectionId;
        }



        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
