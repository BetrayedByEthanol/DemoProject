using DemoProject.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DemoProject.Backend.Hubs
{
    public class BugHub : Hub
    {
        public Task sentData(string data, string clients)
        {

            return Clients.All.SendAsync("Method", data);
        }

        [Authorize]
        public async Task create(BugModel model)
        {
            //Store in db

            await Clients.All.SendAsync("update", model);
            Debug.WriteLine(model.name);
        }
    }
}
