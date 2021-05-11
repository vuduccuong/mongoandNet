using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace App.API.Hubs
{
    public class CountDownHub : Hub<IPushNumber>
    {
         const string GROUP_NAME = "GAME";

        public async Task PushNumber()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GROUP_NAME);
            int _tick = 10; // 10s
            while (_tick >= 0)
            {
                await Clients.Group(GROUP_NAME).PushNumberToClient(_tick, GROUP_NAME);
                _tick--;
                await Task.Delay(1000);
            }
        }
    }
}