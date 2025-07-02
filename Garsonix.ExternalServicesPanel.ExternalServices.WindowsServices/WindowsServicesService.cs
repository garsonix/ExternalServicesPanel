using Garsonix.ExternalServicesPanel.ExternalServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.Versioning;

namespace Garsonix.ExternalServicesPanel.ExternalServices.WindowsServices;

[SupportedOSPlatform("windows")]
public class WindowsServicesService : IExternalServicesService
{
    public List<IExternalServiceMonitor> Services { get; }

    public WindowsServicesService()
    {
        Services = [.. GetWindowsServiceMonitors()];
    }

    private static IEnumerable<IExternalServiceMonitor> GetWindowsServiceMonitors()
    {
        ManagementObjectCollection services;
        try
        {
            var querySearch = new ManagementObjectSearcher("SELECT * FROM Win32_Service");
            services = querySearch.Get();
        }
        catch
        {
            yield break;
        }

        foreach (var serviceObject in services.Cast<ManagementObject>())
        {
            yield return new WindowsServiceMonitor(serviceObject);
        }
    }




    public IExternalServiceMonitor? GetServiceByName(String name)
    {
        return Services.FirstOrDefault(s => s.DisplayName == name);
    }
}
