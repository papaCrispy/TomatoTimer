using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TomatoTimer.Commands
{
    public class RelayCommand : ICommand
    {

        private Action<object> _execute;
        private Predicate<object> _canExecute;
        
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute) : this(execute, null)
        {

        }

        public bool CanExecute(object? parameter)
        {
            return parameter == null ? true : true;
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
