using System.Windows.Input;

namespace AvaloniaApplication2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public ICommand ClickCommand { get; }

    public MainViewModel()
    {
        ClickCommand = new RelayCommand(() =>
        ClickCount++);
    }

    public int ClickCount
    {
        get => GetValue<int>();
        set
        {
            SetValue(value);
            Greeting = $"Click count: {value}";
        }
    }

    public string Greeting
    {
        get => GetValue<string>() ?? $"Click count: {ClickCount}";
        set => SetValue(value);
    }
}
