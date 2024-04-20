using ancanet.server.SignalR.Contracts;
using ancanet.server.SignalR.HubClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ancanet.server.SignalR.Hubs
{
    [Authorize]
    public class ChatHub(IConnectionMapper<string> connections): Hub<IChatHubClient>
    {
        public async void SendChatMessage(string who, string message)
        {
            var sender = Context.User!.Identity!.Name!;
            var connectionIds = connections.GetConnections(who);

            await Clients.Clients(connectionIds).ReceiveMessage(sender, message);
        }

        public override async Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name!;

            var isNewConnection = connections.Add(name, Context.ConnectionId);

            if (isNewConnection)
            {
                await Clients.Others.GetActiveUserNotification(name, $"{name} has joined!");
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            string name = Context.User.Identity.Name;
             
            if (connections.TryRemove(name, Context.ConnectionId, out var isConnectionRemaining) && (isConnectionRemaining ?? false))
            {
                await Clients.Others.GetInActiveUserNotification(name, $"{name} has Leaved!");
            }
        }
    }
}
