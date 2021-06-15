using System.Collections.ObjectModel;

namespace DemoProject.Core.ViewModels
{
    public class BugReportDialogViewModel : InputViewModel
    {
        private ObservableCollection<string> _rating;

        public ObservableCollection<string> rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("name"); }
        }

        public BugReportDialogViewModel()
        {
            rating = new ObservableCollection<string>();
        }
    }
}
