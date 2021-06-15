using DemoProject.Core.Commands;
using System.Windows.Input;

namespace DemoProject.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public bool isLoggedIn { get; set; }
        public ICommand loginCommand { get; set; }
        public LoginViewModel() : base()
        {
            loginCommand = new RelayCommand(login);
        }

        private void login(object sender)
        {

            isLoggedIn = true;
        }

    }
}
