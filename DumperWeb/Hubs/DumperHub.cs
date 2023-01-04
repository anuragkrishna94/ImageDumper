using Microsoft.AspNetCore.SignalR;

namespace DumperWeb.Hubs
{
    public class DumperHub : Hub
    {
        public async Task AddToGroup(string dumperName, string? userName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, dumperName);

            await Clients.Group(dumperName).SendAsync("Notify", $"{userName ?? Context.ConnectionId} has joined the dumper");
        }

        public async Task RemoveFromGroup(string dumperName, string? userName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, dumperName);

            await Clients.Group(dumperName).SendAsync("Notify", $"{userName ?? Context.ConnectionId} has left the dumper");
        }
    }
}
