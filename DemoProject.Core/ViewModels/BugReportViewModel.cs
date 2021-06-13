using DemoProject.Core.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.ViewModels
{
    public class BugReportViewModel : BidirectionalViewModel
    {
        
        private ObservableCollection<BugModel> _bugs;
        public ObservableCollection<BugModel> bugs
        {
            get { return _bugs; }
            set { _bugs = value; }
        }

        public BugReportViewModel()
        {
            bugs = new ObservableCollection<BugModel>();
        }
        public BugReportViewModel(List<BugModel> models)
        {
            bugs = new ObservableCollection<BugModel>();
            foreach (var bug in models) bugs.Add(bug);
        }

        protected override void configureConnectionChannels()
        {

            connection.On<BugModel>("update", data =>
            {
                var bug = bugs.FirstOrDefault(bug => bug.id == data.id);

                if (bug is not null) bug = data;
                else bugs.Add(data);

            });
        }

        public async Task submitBug(BugModel model) => await connection.SendAsync("create", model);
    }
}
