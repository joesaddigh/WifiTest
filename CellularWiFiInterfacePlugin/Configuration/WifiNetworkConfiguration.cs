using Android.Net.Wifi;

namespace CellWiFiInterfacePlugin.Android
{
    public class WifiNetworkConfiguration
    {
        public string Ssid { get; set; }
        public string Passphrase { get; set; }

        public WifiNetworkConfiguration(string ssid, string passphrase)
        {
            Ssid = ssid;
            Passphrase = passphrase;
        }

        public WifiConfiguration CreateWifiConfiguration()
        {
            return new WifiConfiguration()
            {
                Ssid = FormatWifiConfigurationValue(Ssid),
                PreSharedKey = FormatWifiConfigurationValue(Passphrase),
                StatusField = WifiStatus.Enabled
            };
        }

        public static string FormatWifiConfigurationValue(string wifiConfigurationValue)
        {
            return $"\"{wifiConfigurationValue}\"";
        }
    }
}