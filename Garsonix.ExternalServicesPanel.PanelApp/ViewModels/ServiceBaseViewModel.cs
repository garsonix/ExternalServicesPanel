using Garsonix.ExternalServicesPanel.ExternalServices;
using System;
using System.Threading.Tasks;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels;

public abstract class ServiceBaseViewModel(IExternalServiceMonitor serviceMonitor)
    : ObservableViewModelBase
{
    protected readonly IExternalServiceMonitor _serviceMonitor = serviceMonitor;

    public string Name
    {
        get
        {
            return _name;
        }
        protected set
        {
            if (_name == value)
            {
                return;
            }
            
            _name = value;
            RaisePropertyChanged(nameof(Name));
        }
    }
    private string _name = "Unknown";

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

    public bool Running
    {
        get
        {
            return _running;
        }
        set
        {
            if (_running == value)
            {
                return;
            }

            // UI Bound property doing something async (may mess up stuff
            ChangeServiceStatus(value).GetAwaiter().GetResult();
            _running = value;
            RaisePropertyChanged(nameof(Running));
        }
    }
    private bool _running;

    private async Task ChangeServiceStatus(bool targetRunningStatus)
    {
        ReadyToChange = false;
        try
        {
            if (targetRunningStatus)
            {
                await _serviceMonitor.Start();
            }
            else
            {
                await _serviceMonitor.Stop();
            }
        }
        catch (Exception ex)
        {
            ReadyToChange = true;
            throw new Exception("Could not do that", ex);
            // UserMessages.Text = ex.InnerException.Message;
        }
        ReadyToChange = true;
    }
}
