using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.ViewModels
{
    public abstract class BidirectionalViewModel : BaseViewModel
    {

        protected HubConnection connection;
        public async void conntectToHubAsync(string server, string hub)
        {
            connection = new HubConnectionBuilder()
                .WithUrl($"{server}/{hub}")
                .WithAutomaticReconnect()
                .Build();

            configureConnectionChannels();
            connection.Closed += disconnected;
            try {  await connection.StartAsync(); }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
        }

        private Task disconnected(Exception arg)
        {
            return Task.Run(() => { Debug.WriteLine(arg.Message); });
        }

        protected abstract void configureConnectionChannels();

        public async Task disconnectHub() => await connection.StopAsync();
    }
}
