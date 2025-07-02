using Garsonix.ExternalServicesPanel.PanelApp.ViewModels;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Garsonix.ExternalServicesPanel.PanelApp.Services.UI;

public class WindowService
{
    public async Task ShowDialogAsync(UserControl content, XamlRoot xamlRoot)
    {
        var dialog = new ContentDialog
        {
            Title = "About",
            Content = content,
            CloseButtonText = "Close",
            XamlRoot = xamlRoot
        };

        await dialog.ShowAsync();
    }

    public void LaunchDetachedWindow<TPage>(object? parameter) where TPage : Page
    {
        var appWindow = AppWindow.Create();

        var hwndSource = new DesktopWindowXamlSource();
        var frame = new Frame();
        if(parameter is IClosable closableViewModel)
        {
            closableViewModel.Close += (_, _) => 
            {
                // Todo: I don't really want to hide, but to Close seems troublesome
                appWindow.Hide();
                hwndSource.Dispose();
            };
        }

        frame.Navigate(typeof(TPage), parameter);
        hwndSource.Content = frame;

        // Attach content manually
        hwndSource.Initialize(appWindow.Id); // Corrected method to initialize the DesktopWindowXamlSource

        // Fix: Replace the non-existent 'Closed' event with 'Closing' event
        appWindow.Closing += (_, _) => hwndSource.Dispose();
        appWindow.Show();
    }
}