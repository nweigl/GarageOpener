using Xamarin.Essentials;

namespace GarageOpener.Common
{
    public static class AppSettings
    {
        private const string connectionString = "ConnectionString";
        private const string deviceId = "DeviceId";

        public static string ConnectionString => Preferences.Get(connectionString, "");

        public static string DeviceId => Preferences.Get(deviceId, "");

        public static void SetConnectionString(string value)
        {
            Preferences.Set(connectionString, value);
        }

        public static void SetDeviceId(string value)
        {
            Preferences.Set(deviceId, value);
        }
    }
}
