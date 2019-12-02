using System.Threading.Tasks;
using GarageOpener.ViewModels;

namespace GarageOpener.Services
{
    public interface INavigationService
    {
        Task NavigateBack();
        Task NavigateTo<TViewModel>() where TViewModel : BaseViewModel;
    }
}
