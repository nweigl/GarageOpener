using System.ComponentModel;
using GarageOpener.ViewModels;
using Xamarin.Forms;

namespace GarageOpener
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
