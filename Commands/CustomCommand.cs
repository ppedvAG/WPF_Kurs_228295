using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Commands
{
    public class CustomCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CustomCommand(Action<object> exe, Func<object, bool> can)
        {
            ExecuteMethode = exe;
            CanExecuteMethode = can;
        }

        public Action<object> ExecuteMethode { get; set; }
        public Func<object,bool> CanExecuteMethode { get; set; }

        public bool CanExecute(object? parameter) => CanExecuteMethode(parameter);

        public void Execute(object? parameter) => ExecuteMethode(parameter);
    }
}
