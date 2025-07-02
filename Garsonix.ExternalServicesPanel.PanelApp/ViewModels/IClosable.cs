using System;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels;

/// <summary>
/// A UI element that can be closed.
/// </summary>
public interface IClosable
{
    Action<object, object>? Close { get; set; }
}