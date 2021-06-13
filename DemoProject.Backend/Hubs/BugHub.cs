using DemoProject.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoProject.Backend.Hubs
{
    public class BugHub : Hub
    {
        public Task sentData(string data, string clients)
        {

            return Clients.All.SendAsync("Method", data);
        }

        public async Task create(BugModel model)
        {
            //Store in db

            await Clients.All.SendAsync("update", model);
            Debug.WriteLine(model.name);
        }
    }
}
