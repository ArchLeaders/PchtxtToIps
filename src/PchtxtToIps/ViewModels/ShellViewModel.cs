using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentAvalonia.UI.Controls;
using PchtxtToIps.Core;
using System.Collections.ObjectModel;

namespace PchtxtToIps.ViewModels;

public partial class ShellViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<string> _targets = [];

    [ObservableProperty]
    private string _outputFolder = Directory.GetCurrentDirectory();

    [RelayCommand]
    private async Task Browse()
    {
        IReadOnlyList<IStorageFolder> results = await App.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions {
            AllowMultiple = false,
            Title = "Open Output Folder"
        });

        if (results.Count == 0) {
            return;
        }

        OutputFolder = results[0].Path.LocalPath;
    }

    [RelayCommand]
    private async Task Convert()
    {
        foreach (string input in Targets) {
            string ext = Path.GetExtension(input);

            try {
                if (ext is ".ips") {
                    NsoPatch patch = NsoPatch.FromIpsFile(input);
                    patch.WritePchtxt(Path.Combine(OutputFolder, $"{Path.GetFileNameWithoutExtension(input.AsSpan())}.pchtxt"));
                    continue;
                }

                if (ext is ".pchtxt") {
                    NsoPatch patch = NsoPatch.FromPchtxtFile(input);
                    patch.WriteIps(OutputFolder);
                    continue;
                }
            }
            catch (Exception ex) {
                TaskDialog dialog = new() {
                    Title = "Error",
                    Content = new TextBlock {
                        Text = ex.ToString(),
                        FontSize = 12,
                        TextWrapping = TextWrapping.WrapWithOverflow
                    },
                    Buttons = [
                        TaskDialogButton.OKButton,
                        TaskDialogButton.CancelButton
                    ],
                    XamlRoot = App.XamlRoot
                };

                if (await dialog.ShowAsync() is TaskDialogStandardResult.Cancel) {
                    return;
                }
            }
        }
    }

    [RelayCommand]
    private void RemoveItem(string target)
    {
        Targets.Remove(target);
    }
}
