namespace Garsonix.ExternalServicesPanel.ExternalServices;

public interface IExternalServicesService
{
    List<IExternalServiceMonitor> Services { get; }
}
