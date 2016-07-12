using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VerMobi.viewmodel
{
    public class DelegateCommand : ICommand
    {
        public static Func<object, bool> CanExecuteTrue = obj => true;

        //Durch die Weiterleitung von CanExecuteChanged an 
        //CommandManager.RequerySuggested wird sichergestellt, 
        //dass die WPF-Befehlsinfrastruktur jedes Mal, wenn sie 
        //die integrierten Befehle befragt, alle DelegateCommand-Objekte 
        //fragt, ob sie ausgeführt werden können.

        private readonly Action<object> _executeDelegate;
        private readonly Func<object, bool> _canExecuteDelegate;


        public DelegateCommand(Action<object> executeDelegate)
            : this(executeDelegate, null)
        {

        }

        public DelegateCommand(Action<object> executeDelegate, Func<object, bool> canExecuteDelegate)
        {
            _executeDelegate = executeDelegate;
            _canExecuteDelegate = canExecuteDelegate;
        }


        public bool CanExecute(object parameter)
        {
            return _canExecuteDelegate == null || _canExecuteDelegate(parameter);
        }


        public void Execute(object parameter)
        {
            _executeDelegate(parameter);
        }


        public event EventHandler CanExecuteChanged //see http://msdn.microsoft.com/de-de/magazine/dd419663.aspx#id0090030
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }


}
