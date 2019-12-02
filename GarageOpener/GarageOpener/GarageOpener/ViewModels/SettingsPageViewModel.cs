using System.Windows.Input;
using GarageOpener.Common;
using GarageOpener.Services;
using Xamarin.Forms;

namespace GarageOpener.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private string connectionString;
        private string deviceId;

        public SettingsPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ConnectionString = AppSettings.ConnectionString;
            DeviceId = AppSettings.DeviceId;
        }

        public string ConnectionString
        {
            get 
            { 
                return connectionString; 
            }
            set 
            { 
                if (connectionString != value)
                {
                    connectionString = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DeviceId
        {
            get 
            { 
                return deviceId; 
            }
            set 
            { 
                if (deviceId != value)
                {
                    deviceId = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SaveCommand => new Command(SaveCommandExecute);

        private void SaveCommandExecute()
        {
            AppSettings.SetConnectionString(connectionString);
            AppSettings.SetDeviceId(deviceId);
            MessagingCenter.Send<SettingsPageViewModel>(this, "SettingsUpdated");
            _navigationService.NavigateBack();
        }
    }
}
