using Microsoft.AspNetCore.SignalR;

namespace P229SignalR.Hubs
{
    public class P229Hub : Hub
    {
        public async Task MesajGonder(string user, string mesaj,string group)
        {
            await Clients.Group(group).SendAsync("MesajQebulEden", user, mesaj);
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);

            await Clients.OthersInGroup(group).SendAsync("notify", $"{Context.ConnectionId} {group} - a Qosuldu");
        }
    }
}
