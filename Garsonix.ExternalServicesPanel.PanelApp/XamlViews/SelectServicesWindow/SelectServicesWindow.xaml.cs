using Garsonix.ExternalServicesPanel.PanelApp.ViewModels.SelectServicesWindow;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace Garsonix.ExternalServicesPanel.PanelApp.XamlViews.SelectServicesWindow
{
    /// <summary>
    /// Interaction logic for SelectServicesWindow.xaml
    /// </summary>
    public partial class SelectServicesWindow : Page
    {
        private bool _loaded = false;

        public SelectServicesWindow()
        {
            InitializeComponent();
            SearchBox.Loaded += (s, e) =>
            {
                SearchBox.Focus(FocusState.Programmatic);
            };
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_loaded || e.Parameter is not SelectServicesViewModel viewModel) return;

            DataContext = viewModel;

            // Todo: Should I be only doing init once, or does it need to happen every time?
            await viewModel.InitServiceList();
            
            //ShowLoadingBar.Visibility = Visibility.Collapsed;
            _loaded = true;
        }
    }
}