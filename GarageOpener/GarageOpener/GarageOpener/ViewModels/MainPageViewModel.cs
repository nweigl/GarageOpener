using System.Windows.Input;
using GarageOpener.Common;
using GarageOpener.Services;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Xamarin.Forms;

namespace GarageOpener.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private ServiceClient client;
        private RegistryManager registryManager;
        private string connectionString;
        private string deviceId;
        private string doorOneStatus;
        private string doorTwoStatus;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            connectionString = AppSettings.ConnectionString;
            deviceId = AppSettings.DeviceId;

            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(deviceId))
            {
                client = ServiceClient.CreateFromConnectionString(connectionString);
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                GetDeviceTwinState();

                MessagingCenter.Subscribe<App>(this, "RefreshTimerElapsed", (sender) => GetDeviceTwinState());
            }

            MessagingCenter.Subscribe<SettingsPageViewModel>(this, "SettingsUpdated", (sender) => SettingsUpdated());
        }

        public ICommand DoorOneCommand => new Command(DoorOneExecute, (s) => IsRegistered);
        public ICommand DoorTwoCommand => new Command(DoorTwoExecute, (s) => IsRegistered);
        public ICommand SettingsCommand => new Command(SettingsExecute);

        public string DoorOneStatus
        {
            get 
            { 
                return doorOneStatus; 
            }
            set 
            { 
                if (doorOneStatus != value)
                {
                    doorOneStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DoorTwoStatus
        {
            get
            {
                return doorTwoStatus;
            }
            set
            {
                if (doorTwoStatus != value)
                {
                    doorTwoStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRegistered => !string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(deviceId);

        private async void DoorTwoExecute(object obj)
        {
            var result = await client.InvokeDeviceMethodAsync(deviceId, new CloudToDeviceMethod("CycleDoorTwo"));
        }

        private async void DoorOneExecute(object obj)
        {
            var result = await client.InvokeDeviceMethodAsync(deviceId, new CloudToDeviceMethod("CycleDoorOne"));
        }

        private async void SettingsExecute(object obj)
        {
            await _navigationService.NavigateTo<SettingsPageViewModel>();
        }

        private async void GetDeviceTwinState()
        {
            Twin twin = await registryManager.GetTwinAsync(deviceId);

            if (twin == null)
            {
                await App.Current.MainPage.DisplayAlert("Connection Error", "Door status could not be read.", "OK");
                return;
            }

            DoorOneStatus = twin.Properties.Reported["DoorOneStatus"];
            DoorTwoStatus = twin.Properties.Reported["DoorTwoStatus"];
        }

        private async void SettingsUpdated()
        {
            connectionString = AppSettings.ConnectionString;
            deviceId = AppSettings.DeviceId;

            if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(deviceId))
            {
                if (client != null)
                {
                    await client.CloseAsync();
                }

                if (registryManager != null)
                {
                    await registryManager.CloseAsync();
                }

                client = ServiceClient.CreateFromConnectionString(connectionString);
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
                GetDeviceTwinState();

                MessagingCenter.Subscribe<App>(this, "RefreshTimerElapsed", (sender) => GetDeviceTwinState());
            }

            OnPropertyChanged(nameof(IsRegistered));
            ((Command)DoorOneCommand).ChangeCanExecute();
            ((Command)DoorTwoCommand).ChangeCanExecute();
        }
    }
}
