using Microsoft.Win32;
using System.Management;
using System.Runtime.Versioning;
using System.ServiceProcess;

namespace Garsonix.ExternalServicesPanel.ExternalServices.WindowsServices;

/// <summary>
/// Monitors a windows service
/// </summary>
/// <remarks>
/// Based on: http://www.codeproject.com/Tips/703289/How-to-Control-a-Windows-Service-from-Code
/// </remarks>
[SupportedOSPlatform("windows")]
public class WindowsServiceMonitor : IExternalServiceMonitor
{
    /// <summary>
    /// The Windows service that is controlled through the .NET ServiceController
    /// </summary>
    private readonly ServiceController _service;
    private static readonly WindowsServicesService ServicesService;

    public event EventHandler? ServiceUpdated;

    static WindowsServiceMonitor()
    {
        ServicesService = new WindowsServicesService();
    }

    public WindowsServiceMonitor(ManagementObject serviceObject)
    {
        var name = Convert.ToString(serviceObject.GetPropertyValue("Name"));
        var displayName = Convert.ToString(serviceObject.GetPropertyValue("DisplayName"));
        var description = Convert.ToString(serviceObject.GetPropertyValue("Description"));
        var startMode = Convert.ToString(serviceObject.GetPropertyValue("StartMode"));
        var state = Convert.ToString(serviceObject.GetPropertyValue("State"));

        // Todo: Can we get above details from service controller?
        _service = new ServiceController(name);
    }

    #region Properties
    /// <summary>
    /// Name of the Windows service
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    /// Tells us if the Windows service is running
    /// </summary>
    public bool IsRunning => _service.Status == ServiceControllerStatus.Running;

    /// <summary>
    /// Tells us if the Windows service is stopped
    /// </summary>
    public bool IsStopped => _service.Status == ServiceControllerStatus.Stopped;

    /// <summary>
    /// Tells us if the Windows Service is disabled
    /// </summary>
    public bool IsDisabled
    {
        get
        {
            var serviceInfo = ServicesService.GetServiceByName(_service.ServiceName);
            return false;
            //return serviceInfo?.StartMode == "Disabled";
        }
    }

    /// <summary>
    /// Can be called to enable the Windows Service
    /// </summary>
    public void Enable()
    {
        try
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + ServiceName, true);
            key?.SetValue("Start", 2);
        }
        catch (Exception e)
        {
            throw new Exception("Could not enable the service, error: " + e.Message);
        }
    }

    /// <summary>
    /// Disables the Windows service
    /// </summary>
    public void Disable()
    {
        try
        {
            var key = Registry.LocalMachine.OpenSubKey
            (@"SYSTEM\CurrentControlSet\Services\" + ServiceName, true);
            key?.SetValue("Start", 4);
        }
        catch (Exception e)
        {
            throw new Exception("Could not disable the service, error: " + e.Message);
        }
    }

    /// <summary>
    /// This will give us the displayname for the 
    /// Windows Service that we have set in the constructor
    /// </summary>
    public string DisplayName => _service.DisplayName;

    #endregion

    /// <summary>
    /// Start the Windows service, a timeout exception will be thrown when the service
    /// does not start in one minute.
    /// </summary>
    public async Task Start()
    {
        if (_service.Status != ServiceControllerStatus.Running ||
        _service.Status != ServiceControllerStatus.StartPending)
            _service.Start();

        await Task.Run(() => {
            _service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 1, 0));
        });
        ServiceUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Stop the Windows service, a timeout exception will be thrown when the service
    /// does not start in one minute.
    /// </summary>
    public async Task Stop()
    {
        _service.Stop();
        await Task.Run(() => {
            _service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 1, 0));
        });
        ServiceUpdated?.Invoke(this, EventArgs.Empty);
    }

    /// <summary>
    /// Restart the Windows service, a timeout exception will be thrown when the service
    /// does not stop or start in one minute.
    /// </summary>
    public async Task Restart()
    {
        await Stop();
        await Start();
    }

    public async Task Update()
    {
        // This is a no-op for Windows services, as we are using the ServiceController
        // which automatically reflects the current state of the service.
        await Task.CompletedTask;
        ServiceUpdated?.Invoke(this, EventArgs.Empty);
    }
}