using System;
using System.Windows.Input;

namespace RRQM.Emoji
{
    public class DelegateCommand:ICommand
    {
        public Action<object> ExecuteCommand = null;
        public Func<bool> CanExecuteCommand;
        public event EventHandler CanExecuteChanged;
        public Func<object,bool> FuncExecuteCommand;

        public bool FuncExecute(object param)
        {
            if (this.FuncExecuteCommand != null) return this.FuncExecuteCommand(param);
            return false;
        }

        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
        }
 
        public bool CanExecute(object parameter)
        {
            if (this.CanExecuteCommand != null)
            {
                return this.CanExecuteCommand();
            }
            else
            {
                return true;
            }
        }
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public DelegateCommand()
        { }
        public DelegateCommand(Func<bool> canExecuteCommand)
        {
            this.CanExecuteCommand = canExecuteCommand;
        }
        public DelegateCommand(Action<object> executeCommand)
        {
            this.ExecuteCommand = executeCommand;
        }

        public DelegateCommand(Func<object,bool> funcExecuteCommand)
        {
            this.FuncExecuteCommand = funcExecuteCommand;
        }
    }
}
