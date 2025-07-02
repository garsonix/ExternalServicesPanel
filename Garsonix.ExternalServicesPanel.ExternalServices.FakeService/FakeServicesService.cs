namespace Garsonix.ExternalServicesPanel.ExternalServices.FakeService;

public class FakeServicesService : IExternalServicesService
{
    public List<IExternalServiceMonitor> Services { get; }
    
    public FakeServicesService()
    {
        Services =
        [
            new FakeServiceMonitor("Fake Service 1"),
            new FakeServiceMonitor("Fake Service 2"),
            new FakeServiceMonitor("Fake Service 3"),
            new FakeServiceMonitor("Fake Service 4"),
            new FakeServiceMonitor("Fake Service 5")
        ];
    }
}
