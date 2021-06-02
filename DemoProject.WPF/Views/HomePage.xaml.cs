using DemoProject.Core.Models;
using DemoProject.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePageViewModel mViewModel { get; set; }
        public HomePage()
        {
            mViewModel = new HomePageViewModel();
            this.DataContext = mViewModel;
            InitializeComponent();
            mViewModel.test.Add(new TicketModel());
            Debug.WriteLine(mViewModel.test.Count);
            addButton.Click += OpenDialog;
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new BugReportDialog();
            dialog.ShowDialog();
        }
    }
}
