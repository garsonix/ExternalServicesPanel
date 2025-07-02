using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;

namespace Garsonix.ExternalServicesPanel.PanelApp.XamlViews.ValueConverters;

public class BooleanToRunningStoppedConverter : IValueConverter
{
    private const string TrueWord = "Running";
    private const string TrueWordLower = "running";
    private const string FalseWord = "Stopped";
    private const string FalseWordLower = "stopped";

    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;

        var boolValue = System.Convert.ToBoolean(value);
        return boolValue ? TrueWord : FalseWord;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return DependencyProperty.UnsetValue;

        return value.ToString().Trim().ToLower() switch
        {
            TrueWordLower => true,
            FalseWordLower => false,
            _ => (object)false
        };
    }
}