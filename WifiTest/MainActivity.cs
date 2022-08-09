using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using CellWiFiInterfacePlugin.Android;
using System;
using System.Timers;

namespace WifiTest
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private readonly Timer wifiStatusTimer = new Timer(2000)
        {
            AutoReset = true,
            Enabled = true
        };

        private readonly Wifi wifi = new Wifi();

        enum AddRemove { add, remove };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            AddRemoveEventHandlers(AddRemove.add);
        }

        protected override void OnDestroy()
        {
            AddRemoveEventHandlers(AddRemove.remove);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void AddRemoveEventHandlers(AddRemove addRemove)
        {
            var connectButton = FindViewById<Android.Widget.Button>(Resource.Id.connect);
            var disconnectButton = FindViewById<Android.Widget.Button>(Resource.Id.disconnect);
            var enableButton = FindViewById<Android.Widget.ToggleButton>(Resource.Id.enable);

            switch (addRemove)
            {
                case AddRemove.add:
                    {
                        connectButton.Click += ConnectOnClick;
                        disconnectButton.Click += DisonnectOnClick;
                        enableButton.Click += EnableButtonOnClick;

                        wifiStatusTimer.Elapsed += OnWifiStatusTimedEvent;
                        wifiStatusTimer.AutoReset = true;
                        wifiStatusTimer.Enabled = true;
                    }
                    break;

                case AddRemove.remove:
                    {
                        connectButton.Click -= ConnectOnClick;
                        disconnectButton.Click -= DisonnectOnClick;
                        enableButton.Click -= EnableButtonOnClick;

                        wifiStatusTimer.Elapsed -= OnWifiStatusTimedEvent;
                    }
                    break;
            }
        }

        private void ConnectOnClick(object sender, EventArgs eventArgs)
        {
            var ssidText = FindViewById<Android.Widget.EditText>(Resource.Id.ssid).Text;
            var passphraseText = FindViewById<Android.Widget.EditText>(Resource.Id.passphrase).Text;

            wifi.AddNetwork(new WifiNetworkConfiguration(ssidText, passphraseText));
            wifi.Connect();
        }

        private void DisonnectOnClick(object sender, EventArgs eventArgs)
        {
            wifi.Disconnect();
        }

        private void EnableButtonOnClick(object sender, EventArgs eventArgs)
        {
            var enableWifi = FindViewById<Android.Widget.ToggleButton>(Resource.Id.enable).Checked;
            wifi.SetWifiEnabled(enableWifi);
        }

        private void OnWifiStatusTimedEvent(Object source, ElapsedEventArgs e)
        {
            var wifiEnabledToggleButton = FindViewById<Android.Widget.ToggleButton>(Resource.Id.enable);
            var isWifiEnabled = wifi.IsWifiEnabled;

            if (wifiEnabledToggleButton.Checked != isWifiEnabled)
            {
                wifiEnabledToggleButton.Checked = isWifiEnabled;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
