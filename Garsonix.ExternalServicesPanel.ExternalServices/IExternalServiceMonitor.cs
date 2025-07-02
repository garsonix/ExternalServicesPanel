namespace Garsonix.ExternalServicesPanel.ExternalServices;

public interface IExternalServiceMonitor
{
    event EventHandler? ServiceUpdated;

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
    Task Stop();

    /// <summary>
    /// Check to see if the monitored service status has changed.
    /// </summary>
    Task Update();
}
