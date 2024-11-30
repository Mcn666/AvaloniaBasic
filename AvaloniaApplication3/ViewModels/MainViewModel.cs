using Avalonia.Extensions.Media;
using LibVLCSharp.Shared;

namespace AvaloniaApplication3.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private readonly LibVLC _libVLC = new LibVLC();

    public MainViewModel()
    {
        this.VideoView.MediaPlayer = new MediaPlayer(_libVLC);
    }

    // VideoView 控件中的 Content 可以将媒体播放控制控件添加到VideoView控件中，具体实现请参考源代码或参考 PlayerView 控件的实现
    public VideoView VideoView
    {
        get => videoView;
        set => SetProperty(ref videoView, value);
    }
    private VideoView videoView = new VideoView();

    public string MediaUrl
    {
        get => _mediaUrl;
        set => SetProperty(ref _mediaUrl, value);
    }
    private string _mediaUrl = "https://www.w3school.com.cn/example/html5/mov_bbb.mp4";

    public void Play()
    {
        this.VideoView.MediaPlayer.Stop();
        this.VideoView.MediaPlayer.Play(new Media(this._libVLC, this.MediaUrl, FromType.FromLocation));
    }

    public void Pause()
    {
        this.VideoView.MediaPlayer.Pause();
    }

    public void Stop()
    {
        this.VideoView.MediaPlayer.Stop();
    }
}
