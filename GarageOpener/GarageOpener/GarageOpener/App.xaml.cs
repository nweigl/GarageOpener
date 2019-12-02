using System.Timers;
using Autofac;
using GarageOpener.Modules;
using GarageOpener.Services;
using GarageOpener.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GarageOpener
{
    public partial class App : Application
    {
        private static IContainer container;
        private static readonly ContainerBuilder builder = new ContainerBuilder();
        private static readonly Timer refreshTimer = new Timer(5000);

        public App()
        {
            InitializeComponent();
            DependencyResolver.ResolveUsing(type => container.IsRegistered(type) ? container.Resolve(type) : null);

            MainPage = new NavigationPage(container.Resolve<MainPage>());
            refreshTimer.Elapsed += (s, e) => MessagingCenter.Send<App>(this, "RefreshTimerElapsed");
        }

        protected override void OnStart()
        {
            refreshTimer.Start();
        }

        protected override void OnSleep()
        {
            refreshTimer.Stop();
        }

        protected override void OnResume()
        {
            refreshTimer.Start();
        }

        public static void RegisterModules()
        {
            builder.RegisterModule(new ViewModelModule());
            builder.RegisterModule(new ViewModule());
            builder.RegisterModule(new ServiceModule());
        }

        public static void RegisterViews()
        {
            var viewLocator = container.Resolve<IViewLocator>();
            viewLocator.Register<MainPageViewModel, MainPage>();
            viewLocator.Register<SettingsPageViewModel, SettingsPage>();
        }

        public static void BuildContainer()
        {
            RegisterModules();
            container = builder.Build();
            RegisterViews();
        }
    }
}
