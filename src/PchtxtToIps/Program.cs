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

        try {
            if (args.Length > 0) {
                ProcessCli(args);
                return;
            }

            WindowsOperations.TrySetWindowMode(WindowMode.Hidden);
            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex);
            Console.ResetColor();

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

    private static void ProcessCli(string[] args)
    {
        int excludeIndex = -1;
        string output = Directory.GetCurrentDirectory();

        for (int i = 0; i < args.Length; i++) {
            string arg = args[i];

            if (arg.ToLower() is "-o" or "--output" && ++i < args.Length) {
                excludeIndex = i - 1;
                output = args[i];
                continue;
            }
        }

        for (int i = 0; i < args.Length; i++) {
            if (i == excludeIndex || i == excludeIndex + 1) {
                continue;
            }

            string input = args[i];
            // TODO: convert input into output
        }
    }
}
