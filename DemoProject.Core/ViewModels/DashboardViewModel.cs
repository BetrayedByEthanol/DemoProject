namespace DemoProject.Core.ViewModels
{
    public class DashboardViewModel : BaseViewModel
    {
        private BaseViewModel _pageViewModel { get; set; }

        public BaseViewModel pageViewModel
        {
            get
            {
                return _pageViewModel;
            }
            set
            {
                _pageViewModel = value;
                OnPropertyChanged("page");
            }
        }


    }
}
