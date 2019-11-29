using System.Timers;
using System.Windows.Input;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Shared;
using Xamarin.Forms;

namespace GarageOpener.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        private ServiceClient client;
        private RegistryManager registryManager;
        private string connectionString = "<INSERT IOT HUB CONNECTION STRING HERE>";
        private string deviceId = "<INSERT DEVICE ID HERE>";

        private string doorOneStatus;
        private string doorTwoStatus;

        public MainPageViewModel()
        {
            client = ServiceClient.CreateFromConnectionString(connectionString);
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            Timer timer = new Timer(1000);
            timer.Elapsed += GetDeviceTwinState;
            timer.Start();
        }

        public ICommand DoorOneCommand => new Command(DoorOneExecute);
        public ICommand DoorTwoCommand => new Command(DoorTwoExecute);

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

        private async void DoorTwoExecute(object obj)
        {
            var result = await client.InvokeDeviceMethodAsync(deviceId, new CloudToDeviceMethod("CycleDoorTwo"));
        }

        private async void DoorOneExecute(object obj)
        {
            var result = await client.InvokeDeviceMethodAsync(deviceId, new CloudToDeviceMethod("CycleDoorOne"));
        }

        private async void GetDeviceTwinState(object sender, ElapsedEventArgs e)
        {
            Twin twin = await registryManager.GetTwinAsync(deviceId);

            DoorOneStatus = twin.Properties.Reported["DoorOneStatus"];
            DoorTwoStatus = twin.Properties.Reported["DoorTwoStatus"];
        }
    }
}
