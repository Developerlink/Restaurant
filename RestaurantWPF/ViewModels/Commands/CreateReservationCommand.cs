using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantWPF.ViewModels.Commands
{
    public class CreateReservationCommand : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;

        // Passing in a method in execute and a bool in canExecute.
        public CreateReservationCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null ? true : canExecute(parameter);
            return b;
        }

        // Using the passed in method. The parameter is whatever caused the command to execute. In this case the click of a button.
        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
