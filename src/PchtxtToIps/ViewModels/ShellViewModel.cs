using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveItem(string target)
    {
        Targets.Remove(target);
    }
}
