using System;
using System.Collections.Generic;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.Events;

public class ServicesChangedEventArgs : EventArgs
{
    public ServicesChangedEventArgs(string serviceName, bool selected)
    {
        if (selected)
        {
            ServicesSelected = [ serviceName ];
            ServicesUnSelected = [];
        }
        else
        {
            ServicesSelected = [];
            ServicesUnSelected = [ serviceName ];
        }
    }

    public IList<string> ServicesSelected { get; }
    public IList<string> ServicesUnSelected { get; }
}
