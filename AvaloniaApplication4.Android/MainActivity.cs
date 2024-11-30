using Android.App;
using Android.Content.PM;

using Avalonia;
using Avalonia.Android;

namespace AvaloniaApplication4.Android;

[Activity(
    Label = "AvaloniaApplication4.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    public MainActivity()
    {
        Java.Interop.JniEnvironment.References.GetJavaVM(out var vm);
        LibMpv.Client.LibMpv.UseLibMpv(0).UseLibraryPath("").InitAndroid(vm);
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}
