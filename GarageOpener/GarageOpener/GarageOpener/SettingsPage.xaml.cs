using System.ComponentModel;
using GarageOpener.ViewModels;
using Xamarin.Forms;

namespace GarageOpener
{
    [DesignTimeVisible(false)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(SettingsPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}