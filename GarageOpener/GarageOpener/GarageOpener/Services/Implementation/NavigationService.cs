using System;
using System.Threading.Tasks;
using GarageOpener.ViewModels;
using Xamarin.Forms;

namespace GarageOpener.Services.Implementation
{
    public class NavigationService : INavigationService
    {
        private readonly Lazy<INavigation> _navigation;
        private readonly IViewLocator _viewLocator;

        public NavigationService(
            Lazy<INavigation> navigation,
            IViewLocator viewLocator)
        {
            _navigation = navigation;
            _viewLocator = viewLocator;
        }

        private INavigation Navigation => _navigation.Value;

        public async Task NavigateTo<TViewModel>()
            where TViewModel : BaseViewModel
        {
            var view = _viewLocator.Resolve<TViewModel>();

            await Navigation.PushAsync(view);
        }

        public async Task NavigateBack()
        {
            await Navigation.PopAsync();
        }
    }
}
