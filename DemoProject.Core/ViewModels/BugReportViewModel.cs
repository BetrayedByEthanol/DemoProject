using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.ViewModels
{
    public class BugReportViewModel : InputViewModel
    {
        private ObservableCollection<string> _rating;

        public ObservableCollection<string> rating
        {
            get { return _rating; }
            set { _rating = value; }
        }

        public BugReportViewModel()
        {
            rating = new();
            for (int i = 0; i < Enum.GetValues(typeof(EBugState)).Length - 1; i++)
            {
                rating.Add(((EBugState)(i)).ToString());
            }
           
        }

    }
}
