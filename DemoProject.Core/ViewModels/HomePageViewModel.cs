using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DemoProject.Core.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<TicketModel> _test = new ObservableCollection<TicketModel>();

        public ObservableCollection<TicketModel>  test
        {
            get { return _test; }
            set { _test = value; }
        }

        public HomePageViewModel()
        {
        }
    }
}
