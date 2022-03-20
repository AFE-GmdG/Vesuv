using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Vesuv.Editor.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {

        protected static bool IsDesignTime => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (!String.IsNullOrEmpty(propertyName)) {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
