using System;
using System.Windows.Input;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels;


// http://wpftutorial.net/DelegateCommand.html
public class DelegateCommand(Action<object?> execute, Predicate<object?>? canExecute) : ICommand
{
    private readonly Predicate<object?>? _canExecute = canExecute;
    private readonly Action<object?> _execute = execute;

    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action<object?> execute) : this(execute, null)
    {
    }

    public bool CanExecute(object? parameter)
    {
        return _canExecute == null || _canExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}

