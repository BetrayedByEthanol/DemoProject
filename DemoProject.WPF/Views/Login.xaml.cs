using DemoProject.Core.ViewModels;
using DemoProject.WPF.Controllers;
using DemoProject.WPF.Views;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DemoProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private LoginViewModel mViewModel { get; set; }
        public Login()
        {
            mViewModel = new LoginViewModel();
            this.DataContext = mViewModel;
            InitializeComponent();
            BaseControllerInitializer.configureController(this);
        }

        private void usernameTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (String.IsNullOrEmpty(usernameTextbox.Text) && usernameTextbox.Background.GetType() == typeof(SolidColorBrush))
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage((new Uri(@"resources\images\username.gif", UriKind.Relative)));
                imageBrush.AlignmentX = AlignmentX.Left;
                imageBrush.Stretch = Stretch.None;
                imageBrush.Opacity = 0.5;
                usernameTextbox.Background = imageBrush;
            }
            else
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush();
                solidColorBrush.Color = Color.FromRgb(211, 211, 211);
                solidColorBrush.Opacity = 0.5;
                usernameTextbox.Background = solidColorBrush;
            }
        }

        private void passwordbox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(passwordbox.Password) && passwordbox.Background.GetType() == typeof(SolidColorBrush))
            {
                ImageBrush imageBrush = new ImageBrush();
                imageBrush.ImageSource = new BitmapImage((new Uri(@"resources\images\password.gif", UriKind.Relative)));
                imageBrush.AlignmentX = AlignmentX.Left;
                imageBrush.Stretch = Stretch.None;
                imageBrush.Opacity = 0.5;
                passwordbox.Background = imageBrush;
            }
            else
            {
                SolidColorBrush solidColorBrush = new SolidColorBrush();
                solidColorBrush.Color = Color.FromRgb(211, 211, 211);
                solidColorBrush.Opacity = 0.5;
                passwordbox.Background = solidColorBrush;
            }
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void login(object sender, RoutedEventArgs e)
        {
            mViewModel.loginCommand.Execute(null);
            if (mViewModel.isLoggedIn)
            {
                var oldWindow = Application.Current.MainWindow;
                Application.Current.MainWindow = new Dashboard();
                oldWindow.Close();
                Application.Current.MainWindow.Show();
            }
            else
            {
                Debug.WriteLine("Invalid Login Info Text");
            }
        }

    }
}
