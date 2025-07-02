using Garsonix.ExternalServicesPanel.ExternalServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.MainWindow;

public class ServiceViewModel : ServiceBaseViewModel
{
    private readonly IExternalServiceMonitor _serviceMonitor;

    public ServiceViewModel(IExternalServiceMonitor serviceMonitor)
    {
        _serviceMonitor = serviceMonitor;
        UpdateService();
    }

    public ICommand ChangeServiceStatusCommand => new DelegateCommand(async (a) => await ChangeServiceStatus(a), canExecute: c => true);

    public bool ReadyToChange
    {
        get => _readyToChange;
        set
        {
            if (_readyToChange == value) return;
            _readyToChange = value;
            RaisePropertyChanged(nameof(ReadyToChange));
        }
    }
    private bool _readyToChange = true;

    private async Task ChangeServiceStatus(object context)
    {
        ReadyToChange = false;
        try
        {
            if (_serviceMonitor.IsRunning)
            {
                await _serviceMonitor.Stop();
            }
            else
            {
                await _serviceMonitor.Start();
            }
        }
        catch (Exception ex)
        {
            ReadyToChange = true;
            throw new Exception("Could not do that", ex);
            // UserMessages.Text = ex.InnerException.Message;
        }

        Running = _serviceMonitor.IsRunning;
        ReadyToChange = true;
    }

    public void UpdateService()
    {
        Name = _serviceMonitor.DisplayName;
        Running = _serviceMonitor.IsRunning;
    }
}
