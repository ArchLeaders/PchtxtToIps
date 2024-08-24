using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using PchtxtToIps.ViewModels;

namespace PchtxtToIps;

public partial class App : Application
{
    public static Visual XamlRoot { get; private set; } = null!;
    public static IStorageProvider StorageProvider { get; private set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new ShellView {
                DataContext = new ShellViewModel()
            };

            StorageProvider = desktop.MainWindow.StorageProvider;
            XamlRoot = desktop.MainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
