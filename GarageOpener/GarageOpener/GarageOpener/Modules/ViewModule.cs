using Autofac;

namespace GarageOpener.Modules
{
    public class ViewModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainPage>();
            builder.RegisterType<SettingsPage>();
        }
    }
}
