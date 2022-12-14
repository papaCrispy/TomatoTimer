using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TomatoTimer
{
    public class TimerCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;


        public event EventHandler? CanExecuteChanged;

        public TimerCommand(Action <object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public TimerCommand(Action<object> execute) : this(execute, null)
        {

        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
