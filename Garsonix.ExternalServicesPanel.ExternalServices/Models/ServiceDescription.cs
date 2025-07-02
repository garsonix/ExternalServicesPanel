namespace Garsonix.ExternalServicesPanel.ExternalServices.Models;

/// <summary>
/// A description of an external service.
/// </summary>
public class ServiceDescription(string name, bool running, string startMode, string state)
{
    public string Name { get; } = name;
    public bool Running { get; } = running;
    public string StartMode { get; } = startMode;
    public string State { get; } = state;
}
