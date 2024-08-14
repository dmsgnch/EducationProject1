using System.Windows;
using System.Windows.Input;

namespace EducationProject1.Commands;

/// <summary>
/// Universal command with synchronous execution
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<bool>? _canExecute;

    public RelayCommand(Action<object?> execute, Func<bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

    public async void Execute(object? parameter)
    {
        _execute.Invoke(parameter);
    } 

    private void OnCanExecuteChanged()
    {
        if (Application.Current != null && Application.Current.Dispatcher != null)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => CanExecuteChanged?.Invoke(this, EventArgs.Empty));
            }
        }
    }

    public void RaiseCanExecuteChanged() => OnCanExecuteChanged();
}