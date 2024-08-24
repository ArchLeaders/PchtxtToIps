using Avalonia;
using PchtxtToIps.Helpers;

namespace PchtxtToIps;

public class Program
{
    public static readonly string Title = "PchTxt to IPS Converter";

    [STAThread]
    public static void Main(string[] args)
    {
        Console.Title = Title;

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
