using Garsonix.ExternalServicesPanel.ExternalServices;
using Garsonix.ExternalServicesPanel.PanelApp.ViewModels.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.SelectServicesWindow;

public class SelectServicesViewModel(IExternalServicesService servicesService, IEnumerable<string> selectedServices)
    : ObservableViewModelBase, IClosable
{
    private IList<ServiceViewModel> _allServiceViewModels;
    private readonly IExternalServicesService _servicesService = servicesService;
    private readonly IEnumerable<string> _selectedServices = selectedServices;

    public ObservableCollection<ServiceViewModel> Services { get; } = [];

    public async Task InitServiceList()
    {
        var allServices = await Task.Run(() => _servicesService.Services);
        _allServiceViewModels = CreateServiceViewModels(allServices, OnServiceChanged)
            .OrderByDescending(s => s.Selected)
            .ThenBy(a => a.Name)
            .ToList();

        foreach (var serviceViewModel in _allServiceViewModels)
        {
            Services.Add(serviceViewModel);
        }
    }

    private List<ServiceViewModel> CreateServiceViewModels(IEnumerable<IExternalServiceMonitor> allServices, PropertyChangedEventHandler onServiceSelectedChanged)
    {
        var allServicesViewModels = allServices
           .Select(s => new ServiceViewModel(s))
           .ToList();

        foreach (var curentlySelectedService in _selectedServices)
        {
            var serviceViewModel = allServicesViewModels.FirstOrDefault(s => s.Name == curentlySelectedService);
            if (serviceViewModel == null) continue;
            serviceViewModel.Selected = true;
        }

        foreach (var service in allServicesViewModels)
        {
            service.PropertyChanged += OnServiceChanged;
        }

        return allServicesViewModels;
    }

    public Action<object, object>? Close { get; set; }

    public ICommand CloseWindowCommand => new DelegateCommand(CloseWindow, c => true);

    private void CloseWindow(object? context)
    {
       Close?.Invoke(this, context);
    }

    public string? SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;
            _searchText = value;
            RaisePropertyChanged(nameof(SearchText));
            SearchTextFilterServices(value);
        }
    }
    private string? _searchText;

    /// <summary>
    /// Shows or hides services in the list of services based on whether their name matches the search text
    /// </summary>
    private void SearchTextFilterServices(string? searchText)
    {
        Func<string, bool> isVisible = string.IsNullOrEmpty(searchText)
            ? ((s) => true)
            : ((s) => s.Contains(searchText, StringComparison.CurrentCultureIgnoreCase));

        var services = _allServiceViewModels.Where(s => isVisible(s.Name));

        Services.Clear();
        foreach (var service in services)
        {
            Services.Add(service);
        }   
    }
    
    public event SelectedServicesChangedEventHandler? SelectedServicesChanged;
    public delegate void SelectedServicesChangedEventHandler(object sender, ServicesChangedEventArgs e);
    
    private void OnServiceChanged(object? sender, PropertyChangedEventArgs eventArgs)
    {
        switch(eventArgs.PropertyName)
        {
            case "Selected":
                if (sender is not ServiceViewModel serviceViewModel) return;

                var args = new ServicesChangedEventArgs(serviceViewModel.Name, serviceViewModel.Selected);
                SelectedServicesChanged?.Invoke(this, args);
                break;
        }
    }
}