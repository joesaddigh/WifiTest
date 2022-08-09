using System.Collections.Generic;

namespace CellWiFiInterfacePlugin.Android
{
    public class CellularWifiNetworkConfiguration
    {
        private readonly List<WifiNetworkConfiguration> wifiNetworks = new List<WifiNetworkConfiguration>();
        private readonly CellularNetworkConfiguration cellularNetwork = new CellularNetworkConfiguration();

        public WifiNetworkConfiguration GetPrimaryWifiNetwork()
        {
            return wifiNetworks.Count > 0 ? wifiNetworks[0] : null;
        }
    }
}