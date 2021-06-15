using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public BugReportViewModel mViewModel { get; set; }
        public HomePage()
        {
            mViewModel = PageController.getViewModel("Bugs") as BugReportViewModel;
            this.DataContext = mViewModel;
            InitializeComponent();
            addButton.Click += OpenDialog;
        }

        private void OpenDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new BugReportDialog();
            dialog.Closed += submitBug;
            dialog.ShowDialog();
        }

        private async void submitBug(object sender, EventArgs e) => await mViewModel.submitBug(((BugReportDialog)sender).bug);
    }
}
