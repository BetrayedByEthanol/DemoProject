using DemoProject.Core.Models;
using System.Collections.ObjectModel;

namespace DemoProject.Core.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<TicketModel> _test = new ObservableCollection<TicketModel>();

        public ObservableCollection<TicketModel> test
        {
            get { return _test; }
            set { _test = value; }
        }

        public HomePageViewModel()
        {
        }
    }
}
