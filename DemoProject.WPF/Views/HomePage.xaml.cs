using DemoProject.Core.Models;
using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
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
