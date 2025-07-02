using Garsonix.ExternalServicesPanel.ExternalServices.Models;

namespace Garsonix.ExternalServicesPanel.ExternalServices.FakeService;

public class FakeServicesService : IExternalServicesService
{
    private readonly IExternalServiceMonitor[] _services;
    
    public FakeServicesService()
    {
        _services =
        [
            new FakeServiceMonitor("Fake Service 1"),
            new FakeServiceMonitor("Fake Service 2"),
            new FakeServiceMonitor("Fake Service 3"),
            new FakeServiceMonitor("Fake Service 4"),
            new FakeServiceMonitor("Fake Service 5")
        ];
    }

    public List<ServiceDescription> GetAllServices()
    {
        return [.. _services.Select(s => new ServiceDescription(
            s.DisplayName,
            s.IsRunning,
            "mode?",
            "status?"))];
    }
}
