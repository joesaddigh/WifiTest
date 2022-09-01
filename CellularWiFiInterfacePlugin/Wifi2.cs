using Android.App;
using Android.Content;
using Android.Net.Wifi;

namespace CellWiFiInterfacePlugin.Android
{
    public class Wifi2
    {
        private readonly WifiManager wifiManager;

        public Wifi2()
        {
            wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
        }

        public void Connect(WifiNetworkConfiguration wifiNetworkConfiguration)
        {
            var wifiConfiguration = new WifiConfiguration()
            {
                Ssid = wifiNetworkConfiguration.Ssid,
                PreSharedKey = "\"" + wifiNetworkConfiguration.Passphrase + "\"",
                Priority = wifiNetworkConfiguration.Priority
            };

            var networkId = wifiManager.AddNetwork(wifiConfiguration);
            wifiManager.Disconnect();
            wifiManager.EnableNetwork(networkId, true);
            wifiManager.Reconnect();
        }

        private void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}
