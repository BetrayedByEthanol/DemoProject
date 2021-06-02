using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for BugReportDialog.xaml
    /// </summary>
    public partial class BugReportDialog : Window
    {
        BugReportViewModel mViewModel { get; set; }
        public BugReportDialog()
        {
            mViewModel = new ();
            this.DataContext = mViewModel;
            InitializeComponent();
            closeButton.Click += closeInput;
            BaseControllerInitializer.configureController(this);
        }

        private void closeInput(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
