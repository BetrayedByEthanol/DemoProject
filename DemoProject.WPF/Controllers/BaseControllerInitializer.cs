using System.Windows;
using System.Windows.Input;

namespace DemoProject.WPF.Controllers
{
    public static class BaseControllerInitializer
    {
        public static void configureController(Window windowController)
        {
            windowController.MouseDown += dragWindow;
        }


        private static void dragWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                ((Window)sender).DragMove();
        }
    }
}
