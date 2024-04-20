namespace ancanet.server.SignalR.HubClients
{
    public interface IChatHubClient
    {
        Task ReceiveMessage(string sender, string message);

        Task GetActiveUserNotification(string activeUser, string message);

        Task GetInActiveUserNotification(string inActiveUser, string message);
    }
}
