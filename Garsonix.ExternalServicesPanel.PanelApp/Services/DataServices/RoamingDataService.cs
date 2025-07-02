using Windows.Storage;
using Windows.Foundation.Collections;

namespace Garsonix.ExternalServicesPanel.PanelApp.Services.DataServices;

public class RoamingDataService : ISimpleDataService
{
    private readonly IPropertySet _roamingSettings = ApplicationData.Current.RoamingSettings.Values;

    public T? GetValue<T>(string key)
    {
        if (_roamingSettings.TryGetValue(key, out object? storedValue) &&
            storedValue is T tStoredValue)
        {
            return tStoredValue;
        }
        return default;
    }

    public void SetValue<T>(string key, T value)
    {
        if (value == null)
        {
            _roamingSettings.Remove(key);
        }
        else
        {
            _roamingSettings[key] = value;
        }
    }
}