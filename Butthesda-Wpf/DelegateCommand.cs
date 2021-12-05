using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Butthesda_Wpf
{
    class DelegateCommand : ICommand
    {
        private bool _CanExecute;

        public bool CanExecuteValue
        {
            get { return _CanExecute; }
            set { _CanExecute = value;
                if (this.CanExecuteChanged != null)
                {
                    this.CanExecuteChanged.Invoke(this, new EventArgs());
                }
            }
        }

        readonly Action<object> executeDelegate;

        public DelegateCommand(Action<object> executeDelegate)
        {
            this.executeDelegate = executeDelegate;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return this.CanExecuteValue;
        }

        public void Execute(object parameter)
        {
            this.executeDelegate(parameter);
        }
    }
}
