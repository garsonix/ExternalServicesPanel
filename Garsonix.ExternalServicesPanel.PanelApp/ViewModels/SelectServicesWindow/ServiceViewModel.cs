using Garsonix.ExternalServicesPanel.ExternalServices.Models;

namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels.SelectServicesWindow
{
    public class ServiceViewModel : ServiceBaseViewModel
    {
        public ServiceViewModel(ServiceDescription service)
        {
            Name = service.Name;
            Running = service.Running;
            StartMode = service.StartMode;
            State = service.State;
        }

        public bool Selected
        {
            get => _selected;
            set
            {
                if (_selected == value) return;
                _selected = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }
        private bool _selected;

        public string? StartMode
        {
            get => _startMode;
            set
            {
                if (_startMode == value) return;
                _startMode = value;
                RaisePropertyChanged(nameof(StartMode));
            }
        }
        private string? _startMode;

        public string? State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                RaisePropertyChanged(nameof(State));
            }
        }
        private string? _state;
    }
}
