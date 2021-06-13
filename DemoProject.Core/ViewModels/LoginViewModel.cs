using DemoProject.Core.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Windows.Input;
using System.Runtime.Caching;
using Meziantou.Framework.Win32;

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
