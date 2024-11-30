using System;

using Avalonia;

namespace AvaloniaApplication4.Desktop;

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
            var arch = IntPtr.Size == 8 ? "x86_64" : "x86";
            var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"windows/{arch}");
            LibMpv.Client.LibMpv.UseLibMpv(2).UseLibraryPath(path);
        }
        else if (OperatingSystem.IsLinux())
        {
            var arch = IntPtr.Size == 8 ? "x86_64" : "x86";
            var path = $"/usr/lib/{arch}-linux-gnu";
            LibMpv.Client.LibMpv.UseLibMpv(0).UseLibraryPath(path);
        }
    }
}
