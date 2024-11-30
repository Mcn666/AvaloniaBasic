using HanumanInstitute.LibMpv;
using HanumanInstitute.LibMpv.Avalonia;
using System;

namespace AvaloniaApplication5.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    //public MpvView VideoView { get; set; } = new MpvView();
    public NativeView VideoView { get; set; } = new NativeView(); // 如果需要将媒体控制添加到原生控件中，可以使用 NativeView
    //public SoftwareView VideoView { get; set; } = new SoftwareView();
    //public OpenGlView VideoView { get; set; } = new OpenGlView();

    public MpvContext Mpv => VideoView.MpvContext ?? throw new InvalidOperationException("MpvContext is null.");

    public string MediaUrl
    {
        get => _mediaUrl;
        set => SetProperty(ref _mediaUrl, value);
    }
    private string _mediaUrl = "https://www.w3school.com.cn/example/html5/mov_bbb.mp4";

    public async void Play()
    {
        Stop();
        await Mpv.LoadFile(MediaUrl).InvokeAsync();
    }

    public void Pause() => Pause(null);

    public void Pause(bool? value)
    {
        value ??= !Mpv.Pause.Get()!;
        Mpv.Pause.Set(value.Value);
    }

    public void Stop()
    {
        Mpv.Stop().Invoke();
        Mpv.Pause.Set(false);
    }

    public VideoRenderer Renderer { get; set; }

    public void Software() => Renderer = VideoRenderer.Software;
    public void OpenGl() => Renderer = VideoRenderer.OpenGl;
    public void Native() => Renderer = VideoRenderer.Native;
}
