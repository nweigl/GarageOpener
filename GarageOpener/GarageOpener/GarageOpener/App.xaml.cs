using Autofac;
using GarageOpener.Modules;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace GarageOpener
{
    public partial class App : Application
    {
        static IContainer container;
        static readonly ContainerBuilder builder = new ContainerBuilder();

        public App()
        {
            InitializeComponent();
            DependencyResolver.ResolveUsing(type => container.IsRegistered(type) ? container.Resolve(type) : null);

            MainPage = new NavigationPage(container.Resolve<MainPage>());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static void RegisterModules()
        {
            builder.RegisterModule(new ViewModelModule());
            builder.RegisterModule(new ViewModule());
        }

        public static void BuildContainer()
        {
            RegisterModules();
            container = builder.Build();
        }
    }
}
