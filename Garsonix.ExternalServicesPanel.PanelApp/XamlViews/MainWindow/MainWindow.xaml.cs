using Garsonix.ExternalServicesPanel.ExternalServices;
using Garsonix.ExternalServicesPanel.ExternalServices.FakeService;
using Garsonix.ExternalServicesPanel.ExternalServices.WindowsServices;
using Garsonix.ExternalServicesPanel.PanelApp.Services;
using Garsonix.ExternalServicesPanel.PanelApp.Services.DataServices;
using Garsonix.ExternalServicesPanel.PanelApp.ViewModels.MainWindow;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System;
using System.IO;
using System.Linq;
using System.Threading;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-projet-info.

namespace Garsonix.ExternalServicesPanel.PanelApp.XamlViews.MainWindow;


/// <summary>
/// The main window shown when the application starts.
/// </summary>
public partial class MainWindow : Window
{
    private const string ServicesKey = "ServicesToShow";

    private readonly MainWindowViewModel _viewModel;
    private readonly ISimpleDataService _data = new RoamingDataService();
    private readonly IExternalServicesService _servicesService = new WindowsServicesService();

    public MainWindow()
    {
        _viewModel = new MainWindowViewModel();
        SetIcon();
        Activated += OnActivated;
        Closed += OnClosed;
        InitializeComponent();

        try
        {
            var savedServices = _data.GetValue<string>(ServicesKey);
            var servicesList = savedServices?.Split(';', StringSplitOptions.RemoveEmptyEntries) ?? [];
            foreach (var serviceName in servicesList)
            {
                var monitor = _servicesService.Services.FirstOrDefault(s => s.DisplayName == serviceName);
                if (monitor == null)
                {
                    continue;
                }
                //var monitor = new FakeServiceMonitor(service);
                _viewModel.Services.Add(new ServiceViewModel(monitor));
            }
        }
        catch (Exception e)
        {
            _viewModel.StatusMessage = e.Message;
        }
    }

    /// <summary>
    /// Set the icon for the application window.
    /// (Check this is needed when we add png assets for the app.)
    /// </summary>
    private void SetIcon()
    {
        var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = AppWindow.GetFromWindowId(windowId);

        string iconPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Icon.ico");
        appWindow.SetIcon(iconPath);
    }

    protected void OnActivated(object sender, WindowActivatedEventArgs e)
    {
        ContentRoot.DataContext = _viewModel;
        foreach (var service in _viewModel.Services)
        {
            service.UpdateService();
        }
    }

    protected void OnClosed(object sender, WindowEventArgs e)
    {
        try
        {
            var servicesToSave = string.Join(';', _viewModel.Services.Select(s => s.Name));
            _data.SetValue(ServicesKey, servicesToSave);
        }
        catch (Exception)
        {
            // Don't get too upset if we can't save on exit.   
        }
    }
}
