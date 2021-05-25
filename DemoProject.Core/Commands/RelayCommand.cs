using System;
using System.Windows.Input;

namespace DemoProject.Core.Commands
{
    public class RelayCommand : ICommand
    {
        /// <summary>
        /// The action to run
        /// </summary>
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        /// <summary>
        /// Default constructor
        /// </summary>
        public RelayCommand(Action<object> action, Func<object, bool> canExecute = null)
        {
            _execute = action;
            _canExecute = canExecute;
        }


        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);


        /// <summary>
        /// Executes the commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}
