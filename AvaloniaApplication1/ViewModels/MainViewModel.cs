using ReactiveUI;
using System.Windows.Input;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ICommand ClickCommand { get; }

    public MainViewModel()
    {
        ClickCommand = ReactiveCommand.Create(() =>
        {
            // 当按钮被点击时，这里的代码将被执行。
            ClickCount++;
        });
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
