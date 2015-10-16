using System;
using System.Windows.Input;

namespace DwachWPF.Sample
{
    internal class ActionCommand<T> : ICommand
    {
        private Action<T> _action;

        public ActionCommand(Action<T> action)
        {
            _action = action;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _action != null;
        }

        public void Execute(object parameter)
        {
            if (_action != null)
            {
                _action((T)parameter);
            }
        }
    }
}