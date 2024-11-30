using System;

using Avalonia;
using HanumanInstitute.LibMpv.Core;

namespace AvaloniaApplication5.Desktop;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        InitializeMpvLibrary();
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    /// <summary>
    /// 设置动态链接库的路径
    /// </summary>
    public static void InitializeMpvLibrary()
    {
        if (OperatingSystem.IsWindows())
        {
            // MpvApi.RootPath 的默认值为应用程序的根目录，如果需要修改，请修改此处
            var arch = Environment.Is64BitProcess? "x86_64" : "x86";
            //MpvApi.RootPath = Environment.CurrentDirectory + $"\\windows\\{arch}\\"; // 这个路径和下面的路径一样
            MpvApi.RootPath += $"windows\\{arch}\\";
        }
        else if (OperatingSystem.IsLinux())
        {
            // 暂时没有在 Linux 下测试过
        }
    }
}
