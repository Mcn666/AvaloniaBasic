using Avalonia.Controls;
using Avalonia.Extensions.Media;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using LibVLCSharp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication3.ViewModels
{
    public partial class MediaPlayerViewModel : ViewModelBase
    {
        private readonly LibVLC libVLC = new();

        public MediaPlayerViewModel()
        {
            this.VideoView = new VideoView() { MediaPlayer = new MediaPlayer(libVLC) };
            this.VideoView.MediaPlayer.PositionChanged += MediaPlayer_PositionChanged;
            this.VideoView.MediaPlayer.EndReached += MediaPlayer_EndReached;
            this.MediaPlayerVolume = 100; // 设置默认音量
        }

        private void MediaPlayer_PositionChanged(object? sender, MediaPlayerPositionChangedEventArgs e)
        {
            var media = VideoView.MediaPlayer;
            CurrentPlayPosition = media.Position;   // 当前播放进度，范围0-1
            MediaPlayerDuration = media.Length;     // 媒体总长度，单位毫秒
            MediaPlayerPosition = media.Time;       // 媒体当前播放时间，单位毫秒
        }

        public VideoView VideoView
        {
            get => GetValue<VideoView>() ?? throw new NullReferenceException("VideoView is null");
            set => SetValue(value);
        }

        public double CurrentPlayPosition
        {
            get => GetValue<double>();
            set => SetValue(value);
        }

        public double MediaPlayerVolume
        {
            get => GetValue<double>();
            set
            {
                SetValue(value);
                VideoView.MediaPlayer.Volume = (int)value; // 设置音量
            }
        }

        public double MediaPlayerPosition
        {
            get => GetValue<double>();
            set
            {
                SetValue(value);
                OnPropertyChanged(nameof(MediaPlayerTimeInfo));
            }
        }

        public double MediaPlayerDuration
        {
            get => GetValue<double>();
            set => SetValue(value);
        }

        public string MediaPlayerTimeInfo
        {
            get => $"{TimeFormat(MediaPlayerPosition)} / {TimeFormat(MediaPlayerDuration)}";
        }

        public string MediaFilePath
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }

        public string MediaFileName
        {
            get => GetValue<string>() ?? string.Empty;
            set => SetValue(value);
        }

        public Uri MediaPath
        {
            get => GetValue<Uri>() ?? throw new NullReferenceException("MediaPathUri is null");
            set => SetValue(value);
        }

        public bool IsPlaying
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public bool IsPlayEnd
        {
            get => !GetValue<bool>();
            set => SetValue(!value);
        }

        [RelayCommand]
        public void OpenMedia()
        {
            var storage = TopLevel.GetTopLevel(VideoView)?.StorageProvider; // 获取顶层容器
            if (storage is not null && storage.CanOpen)
            {
                var filters = new List<FilePickerFileType>()
                {
                    new ("媒体文件")
                    {
                        Patterns = ["*.mp4", "avi", "mkv", "wmv", "flv"]
                    }
                };
                var result = storage.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions()
                {
                    FileTypeFilter = filters.AsReadOnly()
                });
                result.ContinueWith(task =>
                {
                    if (task.IsCompleted && task.Result.Count > 0)
                    {
                        MediaPath = task.Result.ElementAt(0).Path; // 获取第一个选择的文件路径
                        MediaFileName = task.Result.ElementAt(0).Name; // 该文件名称
                        MediaFilePath = System.IO.Path.GetDirectoryName(MediaPath.LocalPath) ?? string.Empty; // 该文件所在目录
                        StopMedia(); // 停止当前播放
                        PlayMedia(); // 播放新媒体
                    }
                    else
                    {
                        MediaFilePath = string.Empty;
                    }
                });
            }
        }

        [RelayCommand]
        public void PlayMedia()
        {
            if (string.IsNullOrEmpty(MediaFileName) == false)
            {
                if (IsPlayEnd) // 检查是否播放结束，如果是，则重新播放
                {
                    //VideoView.MediaPlayer.Play(new Media(libVLC, "https://www.w3school.com.cn/example/html5/mov_bbb.mp4", FromType.FromLocation));
                    VideoView.MediaPlayer.Play(new Media(libVLC, MediaPath));
                    IsPlaying = true;
                    IsPlayEnd = false;
                }
                else
                {
                    VideoView.MediaPlayer.Play();
                    IsPlaying = true;
                }

            }
        }

        private void MediaPlayer_EndReached(object? sender, EventArgs e)
        {
            IsPlaying = false;
            IsPlayEnd = true;
        }

        [RelayCommand]
        public void StopMedia()
        {
            VideoView.MediaPlayer.Stop();
            IsPlaying = false;
            IsPlayEnd = true;
        }

        [RelayCommand]
        public void PauseMedia()
        {
            VideoView.MediaPlayer.Pause();
            IsPlaying = false;
        }

        /// <summary>
        /// 格式化时间为 hh:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string TimeFormat(double time)
        {
            time /= 1000; // 转换为秒
            TimeSpan ts = TimeSpan.FromSeconds(time);
            return ts.ToString("hh\\:mm\\:ss");
        }
    }
}
