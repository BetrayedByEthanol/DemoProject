using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
using System.Windows;
using System.Windows.Controls;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public DashboardViewModel mViewModel { get; set; }

        public Dashboard()
        {
            this.Height = SystemParameters.PrimaryScreenHeight * 0.5;
            this.Width = SystemParameters.PrimaryScreenWidth * 0.5;
            mViewModel = new DashboardViewModel();
            this.DataContext = mViewModel;
            InitializeComponent();
            page.Content = PageController.switchTo("Bugs");
            BaseControllerInitializer.configureController(this);
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void TicketsChecked(object sender, RoutedEventArgs e)
        {
            page.Content = PageController.switchTo(((RadioButton)sender).Content.ToString());
        }
        private void BugsChecked(object sender, RoutedEventArgs e)
        {
            page.Content = new HomePage();
        }
    }
}
