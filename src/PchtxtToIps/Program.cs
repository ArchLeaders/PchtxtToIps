using Avalonia;
using PchtxtToIps.Helpers;

namespace PchtxtToIps;

class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        if (args.Length > 0) {
            return;
        }

        try {
            WindowsOperations.TrySetWindowMode(WindowMode.Hidden);
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex) {
            Console.WriteLine(ex);
            WindowsOperations.TrySetWindowMode(WindowMode.Visible);
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
