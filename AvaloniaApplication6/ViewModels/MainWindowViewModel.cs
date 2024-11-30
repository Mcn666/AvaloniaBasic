using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System;

namespace AvaloniaApplication6.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public Bitmap? Bitmap { get; } = new Bitmap("../../../Assets/avalonia-logo.ico"); // 指定路径的图片

        public Bitmap? AssetBitmap { get; } = ImageHelper.LoadFromResource(new Uri("avares://AvaloniaApplication6/Assets/avalonia-logo.ico")); // 包含在项目中的图片

        public Task<Bitmap?> WebBitmap { get; } = ImageHelper.LoadFromWeb(new Uri("https://upload.wikimedia.org/wikipedia/commons/4/41/NewtonsPrincipia.jpg")); // 从网络中获取的图片
    }

    public static class ImageHelper
    {
        public static Bitmap LoadFromResource(Uri resourceUri)
        {
            return new Bitmap(AssetLoader.Open(resourceUri));
        }

        public static async Task<Bitmap?> LoadFromWeb(Uri url)
        {
            using var httpClient = new HttpClient();
            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsByteArrayAsync();
                return new Bitmap(new MemoryStream(data));
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while downloading image '{url}' : {ex.Message}");
                return null;
            }
        }
    }
}
