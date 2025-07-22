using Garsonix.ExternalServicesPanel.ExternalServices;
using Garsonix.ExternalServicesPanel.ExternalServices.FakeService;
using Garsonix.ExternalServicesPanel.ExternalServices.WindowsServices;
using Garsonix.ExternalServicesPanel.PanelApp.Services.UI;
using Garsonix.ExternalServicesPanel.PanelApp.ViewModels.Events;
using Garsonix.ExternalServicesPanel.PanelApp.ViewModels.SelectServicesWindow;
using Garsonix.ExternalServicesPanel.PanelApp.XamlViews.AboutWindow;
using Microsoft.UI.Xaml;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;


namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.MainWindow;

public class MainWindowViewModel : ObservableViewModelBase
{
    private readonly IExternalServicesService ServicesService;
    private readonly WindowService _windowService;

    public MainWindowViewModel()
    {
        ServicesService = new WindowsServicesService();
        _windowService = new WindowService();
    }

    public string? StatusMessage
    {
        get => _statusMessage;
        set
        {
            if (_statusMessage == value) return;
            _statusMessage = value;
            RaisePropertyChanged(nameof(StatusMessage));
        }
    }
    private string? _statusMessage;


    public ObservableCollection<ServiceViewModel> Services { get; private init; } = [];

    public ICommand OpenServicesSelectionWindowCommand => new DelegateCommand(OpenServicesSelectionWindow, c => true);

    private void OpenServicesSelectionWindow(object? context)
    {
        var selectServicesViewModel = new SelectServicesViewModel(ServicesService, Services.Select(s => s.Name));
        selectServicesViewModel.SelectedServicesChanged += OnSelectedServicesChanged;

        _windowService.LaunchDetachedWindow<XamlViews.SelectServicesWindow.SelectServicesWindow>(selectServicesViewModel);
    }

    public ICommand OpenAboutWindowCommand => new DelegateCommand(OpenAboutWindow, c => true);

    private async void OpenAboutWindow(object? parameter)
    {
        if (parameter is FrameworkElement sourceWindow)
        {
            await _windowService.ShowDialogAsync(new AboutWindow(), sourceWindow.XamlRoot);
        }
    }

    private void OnSelectedServicesChanged(object sender, ServicesChangedEventArgs servicesChangedEventArgs)
    {
        foreach (var unselectedServiceName in servicesChangedEventArgs.ServicesUnSelected)
        {
            var service = Services.FirstOrDefault(s => s.Name == unselectedServiceName);
            if (service == null) continue;

            Services.Remove(service);
        }

        foreach (var selectedServiceName in servicesChangedEventArgs.ServicesSelected)
        {
            var service = new ServiceViewModel(new FakeServiceMonitor(selectedServiceName));
            Services.Add(service);
        }
    }
}
