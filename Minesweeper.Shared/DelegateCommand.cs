using System;
using System.Windows.Input;

namespace Minesweeper.Shared
{
    public class DelegateCommand : ICommand
    {
        #region Fields
        private readonly Predicate<object> canExecute;
        private readonly Action<object> execute;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged;
        #endregion

        #region Constructor
        public DelegateCommand(Action<object> execute)
        : this(execute, null)
        {
        }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        #endregion

        #region Methods
        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            return canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
        #endregion
    }
}
