namespace Garsonix.ExternalServicesPanel.PanelApp.ViewModels;

public abstract class ServiceBaseViewModel : ObservableViewModelBase
{
    public string Name
    {
        get
        {
            return _name;
        }
        protected set
        {
            if (_name == value)
            {
                return;
            }
            
            _name = value;
            RaisePropertyChanged(nameof(Name));
        }
    }
    private string _name = "Unknown";

    public bool Running
    {
        get
        {
            return _running;
        }
        set
        {
            if (_running == value)
            {
                return;
            }
            
            _running = value;
            RaisePropertyChanged(nameof(Running));
        }
    }
    private bool _running;
}
