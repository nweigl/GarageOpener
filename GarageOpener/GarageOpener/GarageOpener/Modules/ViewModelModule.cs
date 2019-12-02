using Autofac;
using GarageOpener.ViewModels;

namespace GarageOpener.Modules
{
    public class ViewModelModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<SettingsPageViewModel>();
        }
    }
}
