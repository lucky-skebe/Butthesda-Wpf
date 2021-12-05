using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace Butthesda_Wpf
{
    class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected static void OnUIThread(Action a)
        {
            Application.Current.Dispatcher.Invoke(a);
        }

        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]  string name = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
