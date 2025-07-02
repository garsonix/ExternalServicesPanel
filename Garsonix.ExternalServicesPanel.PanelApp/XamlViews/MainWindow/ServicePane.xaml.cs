using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Garsonix.ExternalServicesPanel.PanelApp.XamlViews.MainWindow;

public partial class ServicePane : UserControl
{
    public ServicePane()
    {
        InitializeComponent();
    }

    static ServicePane()
    {
        RunningProperty =
            DependencyProperty.Register("Running",
                typeof(bool),
                typeof(ServicePane),
                new PropertyMetadata(false));

        ServiceNameProperty =
            DependencyProperty.Register("ServiceName",
                typeof(string),
                typeof(ServicePane),
                new PropertyMetadata("Unknown"));
    }

    public string ServiceName
    {
        get { return (string)GetValue(ServiceNameProperty); }
        set { SetValue(ServiceNameProperty, value); }
    }
    public static DependencyProperty ServiceNameProperty { get; }

    public bool Running
    {
        get { return (bool)GetValue(RunningProperty); }
        set { SetValue(RunningProperty, value); }
    }
    public static DependencyProperty RunningProperty { get; }
}
