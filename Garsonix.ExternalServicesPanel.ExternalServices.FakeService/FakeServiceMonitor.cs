
namespace Garsonix.ExternalServicesPanel.ExternalServices.FakeService;

public class FakeServiceMonitor(string serviceName) : IExternalServiceMonitor
{
    public string DisplayName { get; private set; } = serviceName;

    public bool IsRunning { get; private set; } = false;

    public event EventHandler? ServiceUpdated;

    public Task Start()
    {
        IsRunning = true;
        return Task.CompletedTask;
    }

    public Task Stop()
    {
        IsRunning = false;
        return Task.CompletedTask;
    }

    public Task Update()
    {
        //ServiceUpdated?.Invoke(this, EventArgs.Empty);
        return Task.CompletedTask;
    }
}
