using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DemoProject.Core.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected object mPropertyValueCheckLock = new object();

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



    }
}
