using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Garsonix.ExternalServicesPanel.PanelApp.XamlViews.ValueConverters;

public class BooleanToIsBoldConverter : IValueConverter
{

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;

        var boolValue = System.Convert.ToBoolean(value);
        return boolValue
            ? FontWeights.Bold
            : FontWeights.Normal;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;

        return value?.ToString().Trim().ToLower() switch
        {
            "bold" => true,
            _ => (object)false
        };
    }
}