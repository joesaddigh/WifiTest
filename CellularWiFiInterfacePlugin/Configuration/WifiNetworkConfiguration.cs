using Android.Net.Wifi;

namespace CellWiFiInterfacePlugin.Android
{
    public class WifiNetworkConfiguration
    {
        public string Ssid { get; set; }
        public string Passphrase { get; set; }
        public int Priority { get; set; }

        public WifiNetworkConfiguration(string ssid, string passphrase, int priority)
        {
            Ssid = ssid;
            Passphrase = passphrase;
            Priority = priority;
        }

        public WifiConfiguration CreateWifiConfiguration()
        {
            return new WifiConfiguration()
            {
                Ssid = Ssid
                ,PreSharedKey = FormatWifiConfigurationValue(Passphrase)
                ,StatusField = WifiStatus.Enabled
                ,Priority = Priority
            };
        }

        public static string FormatWifiConfigurationValue(string wifiConfigurationValue)
        {
            return $"\"{wifiConfigurationValue}\"";
        }
    }
}