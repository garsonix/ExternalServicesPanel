using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garsonix.ExternalServicesPanel.PanelApp.Services;

public interface ISimpleDataService
{
    T? GetValue<T>(string key);

    void SetValue<T>(string key, T value);
}
