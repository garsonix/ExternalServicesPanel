using Garsonix.ExternalServicesPanel.ExternalServices.Models;
using System.Collections.Generic;

namespace Garsonix.ExternalServicesPanel.ExternalServices;

public interface IExternalServicesService
{
    List<ServiceDescription> GetAllServices();
}
