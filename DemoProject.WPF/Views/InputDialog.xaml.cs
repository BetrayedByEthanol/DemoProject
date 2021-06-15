using DemoProject.WPF.Controllers;
using System.Windows;

namespace DemoProject.WPF.Views
{
    /// <summary>
    /// Interaction logic for InputDialog.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog()
        {
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
