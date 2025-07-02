namespace Garsonix.ExternalServicesPanel.ExternalServices;

public interface IExternalServiceMonitor
{
    /// <summary>
    /// The name of the external service.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Is the external service currently running?
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Start the external service.
    /// </summary>
    Task Start();

    /// <summary>
    /// Stop the external service.
    /// </summary>
    /// <returns></returns>
    Task Stop();
}
