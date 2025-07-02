using Garsonix.ExternalServicesPanel.ExternalServices;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.MainWindow;

public class ServiceViewModel : ServiceBaseViewModel
{
    public ServiceViewModel(IExternalServiceMonitor serviceMonitor) : base(serviceMonitor)
    {        
        UpdateService();
        _serviceMonitor.ServiceUpdated += (s, e) => UpdateService();
    }


    public void UpdateService()
    {
        Name = _serviceMonitor.DisplayName;
        Running = _serviceMonitor.IsRunning;
    }
}
