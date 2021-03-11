using Contoso.Mobile.UI.Services;
using Contoso.Mobile.UI.Views;
using Xamarin.Forms;

namespace Contoso.Mobile.UI
{
    public partial class App : Application
    {
        private string _NavigationService;
        public string NavigationService
        {
            get { return _NavigationService; }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<NavigationService>();
        }

        protected override async void OnStart()
        {
            var nav = DependencyService.Get<NavigationService>();
            await nav.HomeAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}