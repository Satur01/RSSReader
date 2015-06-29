using System;
using System.Windows.Input;

namespace RSSReader.ViewModels.ViewModelBase
{
    public class DelegateCommand : ICommand
    {

        #region Fields

        private Action _execute;
        private Func<bool> _canExecute;

        #endregion

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute();

        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

    }
}
