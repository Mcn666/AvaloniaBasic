using Avalonia.Threading;
using LibMpv.Avalonia;
using LibMpv.Client;
using LibMpv.MVVM;
using System;

namespace AvaloniaApplication4.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        var url = "https://www.w3school.com.cn/example/html5/mov_bbb.mp4";
        (VideoView.MpvContext as VideoViewModel)?.Play(url);
    }

    // 媒体播放窗口会自动弹出，如果要将其嵌入到控件中，需要定制控件，例如从 NativeControlHost 类派生
    // 具体可以参考 NuGet 包 HanumanInstitute.LibMpv.Avalonia 中的 NativeView 类的实现
    public VideoView VideoView
    {
        get => _video;
        set => SetProperty(ref _video, value);
    }
    private VideoView _video = new() { MpvContext = new VideoViewModel() };
}

public class VideoViewModel : BaseMpvContextViewModel
{
    public void Play(string rtspUrl)
    {
        try
        {
            Console.WriteLine($"LoadFile");
            base.LoadFile(rtspUrl);
            Console.WriteLine($"Play");
            base.Play();
            Console.WriteLine($"Play end");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public override void InvokeInUIThread(Action action)
    {
        Dispatcher.UIThread.Invoke(action);
    }
}