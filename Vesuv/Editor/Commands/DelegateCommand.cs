using System.Windows.Input;

namespace Vesuv.Editor.Commands
{
    public class DelegateCommand : ICommand
    {

        private readonly Action<Object?> _execute;
        private readonly Predicate<Object?>? _canExecute;

        public event EventHandler? CanExecuteChanged;

        public DelegateCommand(Action<Object?> execute) : this(null, execute) { }
        public DelegateCommand(Predicate<Object?>? canExecute, Action<Object?> execute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
        public void Execute(object? parameter) => _execute?.Invoke(parameter);

        protected void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
