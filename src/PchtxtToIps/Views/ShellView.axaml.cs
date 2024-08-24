using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using FluentAvalonia.UI.Windowing;
using PchtxtToIps.ViewModels;

namespace PchtxtToIps;

public partial class ShellView : AppWindow
{
    public ShellView()
    {
        InitializeComponent();

        Bitmap bitmap = new(AssetLoader.Open(new Uri($"avares://{nameof(PchtxtToIps)}/Assets/Icon.ico")));
        Icon = bitmap.CreateScaledBitmap(new(48, 48), BitmapInterpolationMode.HighQuality);

        DropTarget.AddHandler(DragDrop.DragEnterEvent, DragEnterEvent);
        DropTarget.AddHandler(DragDrop.DragLeaveEvent, DragLeaveEvent);
        DropTarget.AddHandler(DragDrop.DropEvent, DragDropEvent);
    }

    public void DragDropEvent(object? sender, DragEventArgs e)
    {
        if (DataContext is not ShellViewModel vm) {
            goto Cleanup;
        }

        if (e.Data.GetFiles() is not { } files) {
            goto Cleanup;
        }

        foreach (string file in files.Select(x => x.Path.LocalPath).Where(File.Exists).Where(x => Path.GetExtension(x.AsSpan()) is ".ips" or ".pchtxt")) {
            vm.Targets.Add(file);
        }

    Cleanup:
        OpacityMask.Opacity = 0.0;
    }

    public void DragEnterEvent(object? sender, DragEventArgs e)
    {
        OpacityMask.Opacity = 0.5;
    }

    public void DragLeaveEvent(object? sender, DragEventArgs e)
    {
        OpacityMask.Opacity = 0.0;
    }
}