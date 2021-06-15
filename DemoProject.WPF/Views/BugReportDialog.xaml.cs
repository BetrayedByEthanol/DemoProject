using DemoProject.Core.Models;
using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
using System;
using System.Windows;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for BugReportDialog.xaml
    /// </summary>
    public partial class BugReportDialog : Window
    {
        BugReportDialogViewModel mViewModel { get; set; }
        public BugModel bug;
        public BugReportDialog()
        {
            mViewModel = new();
            this.DataContext = mViewModel;
            InitializeComponent();
            closeButton.Click += closeInput;
            BaseControllerInitializer.configureController(this);
        }

        private void closeInput(object sender, RoutedEventArgs e)
        {
            bug = new()
            {
                id = Guid.NewGuid(),
                name = "name",
                state = 0,
                description = "desc",
            };
            this.Close();
        }
    }
}
