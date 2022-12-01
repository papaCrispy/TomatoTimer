using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TomatoTimer.CustomMessageBox
{
    public class UserActionsCommands : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public Action<object> _Execute;

        public UserActionsCommands(Action<object> Execute)
        {
            _Execute = Execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _Execute.Invoke(parameter);
        }
    }
}
