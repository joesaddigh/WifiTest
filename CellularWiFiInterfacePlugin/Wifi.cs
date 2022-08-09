using Android.App;
using Android.Content;
using Android.Net.Wifi;

namespace CellWiFiInterfacePlugin.Android
{
    public class Wifi
    {
        private readonly WifiManager wifiManager;
        private int configuredNetworkId;

        private const int UNKNOWN_NETWORK_ID = -1;
        private const string UNKNOWN_SSID = "<unknown ssid>";

        public Wifi()
        {
            wifiManager = (WifiManager)Application.Context.GetSystemService(Context.WifiService);
        }

        public void Connect()
        {
            Disconnect();

            EnableNetwork();

            if (wifiManager.Reconnect() && IsWifiConnected())
            {
                Log("Successfully connected to the wifi network.");
            }
            else
            {
                Log("Failed to connect to the wifi network.");
            }
        }

        public void Disconnect()
        {
            wifiManager.Disconnect();
        }

        public void EnableNetwork()
        {
            var enableNetwork = wifiManager.EnableNetwork(configuredNetworkId, true);
            if (!enableNetwork)
            {
                Log($"Failed to enable network with id: {configuredNetworkId}.");
                return;
            }
        }
        
        public bool AddNetwork(WifiNetworkConfiguration wifiNetworkConfiguration)
        {
            // Just in case our network is already added, remove it first.
            RemoveConfiguredNetwork(wifiNetworkConfiguration);

            // Now add it and return the success state.
            return AddConfiguredNetwork(wifiNetworkConfiguration);
        }

        public bool IsWifiEnabled => wifiManager.IsWifiEnabled;

        public bool SetWifiEnabled(bool enabled)
        {
            return wifiManager.SetWifiEnabled(enabled);
        }

        private void RemoveConfiguredNetwork(WifiNetworkConfiguration primaryWifiNetworkConfiguration)
        {
            if (wifiManager.ConfiguredNetworks == null || wifiManager.ConfiguredNetworks.Count == 0)
            {
                return;
            }

            foreach (var configuredNetwork in wifiManager.ConfiguredNetworks)
            {
                if (configuredNetwork.Ssid == WifiNetworkConfiguration.FormatWifiConfigurationValue(primaryWifiNetworkConfiguration.Ssid))
                {
                    Log($"Removing Wifi Network: {primaryWifiNetworkConfiguration.Ssid}.");
                    wifiManager.RemoveNetwork(configuredNetwork.NetworkId);
                }
            }
        }

        private bool AddConfiguredNetwork(WifiNetworkConfiguration primaryWifiNetworkConfiguration)
        {
            configuredNetworkId = wifiManager.AddNetwork(primaryWifiNetworkConfiguration.CreateWifiConfiguration());

            if (configuredNetworkId < 0)
            {
                Log($"Failed to add primary network: {primaryWifiNetworkConfiguration.Ssid}. Returned network id is: {configuredNetworkId}.");
            }
            else
            {
                Log($"Successfully added configured network: {primaryWifiNetworkConfiguration.Ssid}. Returned network id is: {configuredNetworkId}.");
            }

            return true;
        }

        private bool IsWifiConnected()
        {
            var isWifiConnected = 
                wifiManager.IsWifiEnabled &&
                wifiManager.ConnectionInfo.NetworkId != UNKNOWN_NETWORK_ID && 
                wifiManager.ConnectionInfo.SSID != UNKNOWN_SSID;

            return isWifiConnected;
        }

        private void Log(string log)
        {
            System.Diagnostics.Debug.WriteLine(log);
        }
    }
}
