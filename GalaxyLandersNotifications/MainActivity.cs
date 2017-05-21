using Android.App;
using Android.Widget;
using Android.OS;

using Realms;

using System.Linq;
using System.Threading;

namespace GalaxyLandersNotifications
{
    [Activity(Label = "GalaxyLandersServer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private static TextView logTextView;
        public delegate void LogDelegate(string log);

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            logTextView = FindViewById<TextView>(Resource.Id.textView1);
            logTextView.Text = "";
            
            new Thread(Loop).Start();
            new Thread(new Server((s) => { Log(s); }).Start).Start();
        }

        public void Loop()
        {
            Realm realm = Realm.GetInstance();
            var notifications = realm.All<Notification>();

            while (true)
            {
                realm.Refresh();
                Thread.Sleep(500);
            }
        }

        private void Log(string log)
        {
            RunOnUiThread(() =>
            {
                logTextView.Text += log + "\n";
            });
        }
    }
}

