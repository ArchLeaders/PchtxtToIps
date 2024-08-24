using Avalonia.Media.Imaging;
using Avalonia.Platform;
using FluentAvalonia.UI.Windowing;

namespace PchtxtToIps;

public partial class ShellView : AppWindow
{
    public ShellView()
    {
        InitializeComponent();

        Bitmap bitmap = new(AssetLoader.Open(new Uri($"avares://{nameof(PchtxtToIps)}/Assets/Icon.ico")));
        Icon = bitmap.CreateScaledBitmap(new(48, 48), BitmapInterpolationMode.HighQuality);
    }
}