using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.IO;
using System.Threading.Tasks;

namespace NotificationBridge
{
    public class Controller1 : Controller
    {
        private readonly IHubContext<NotificationHub> hub;
        public Controller1(IHubContext<NotificationHub> hub) => this.hub = hub;

        [HttpPost("notify/{group}")]
        public async Task Notify(string group)
        {
            string payload;
            using (var sr = new StreamReader(Request.Body)) payload = await sr.ReadToEndAsync();
            await hub.Clients.Group(group).SendAsync("Notify", payload);
        }

    }
}
