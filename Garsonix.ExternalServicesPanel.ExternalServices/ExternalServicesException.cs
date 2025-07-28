namespace Garsonix.ExternalServicesPanel.ExternalServices;

public class ExternalServicesException : Exception
{
    public ExternalServicesException(string? message) : base(message)
    {
    }

    public ExternalServicesException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
