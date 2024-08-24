using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace PchtxtToIps.Helpers;

public enum WindowMode : int { Hidden = 0, Visible = 5 }

public static partial class WindowsOperations
{
    private static readonly IntPtr _handle = GetConsoleWindow();

    [LibraryImport("kernel32.dll")]
    private static partial IntPtr GetConsoleWindow();

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool ShowWindow(IntPtr window_handle, int cmd_show_mode);

    public static void TrySetWindowMode(WindowMode mode)
    {
        if (OperatingSystem.IsWindows()) {
            ShowWindow(_handle, (int)mode);
        }
    }

    [SupportedOSPlatform(nameof(OSPlatform.Windows))]
    public static void SetWindowMode(WindowMode mode)
    {
        ShowWindow(_handle, (int)mode);
    }
}