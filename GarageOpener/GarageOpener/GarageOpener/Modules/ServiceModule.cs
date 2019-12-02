using Autofac;
using GarageOpener.Services;
using GarageOpener.Services.Implementation;
using Xamarin.Forms;

namespace GarageOpener.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<ViewLocator>().As<IViewLocator>().SingleInstance();
            builder.Register<INavigation>(context => App.Current.MainPage.Navigation).SingleInstance();
        }
    }
}
