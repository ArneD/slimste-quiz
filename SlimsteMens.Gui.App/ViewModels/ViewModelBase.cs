using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace SlimsteMens.Gui.App.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Dispatch(Action action, DispatcherPriority priority)
        {
            Application.Current.Dispatcher.BeginInvoke(action, priority);
        }
    }
}
