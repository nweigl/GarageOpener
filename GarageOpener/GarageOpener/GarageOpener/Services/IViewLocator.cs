using GarageOpener.ViewModels;
using Xamarin.Forms;

namespace GarageOpener.Services
{
    public interface IViewLocator
    {
        void Register<TViewModel, TView>()
            where TViewModel : BaseViewModel
            where TView : Page;
        Page Resolve<TViewModel>() where TViewModel : BaseViewModel;
    }
}
