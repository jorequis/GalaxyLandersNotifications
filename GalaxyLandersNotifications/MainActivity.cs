using Android.App;
using Android.Widget;
using Android.OS;

using Realms;

using System.Linq;

namespace GalaxyLandersNotifications
{
    [Activity(Label = "GalaxyLandersNotifications", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);

            TextView tv = FindViewById<TextView>(Resource.Id.textView1);
            Realm realm = Realm.GetInstance();

            var userJorge =realm.All<User>().Where(u => u.Name.Equals("Jorge"));
            if (userJorge.Count() >= 1)
                tv.Text = "Encontrao";
            else
            {
                realm.Write(() =>
                {
                    User u = new User { Name = "Jorge" };
                    realm.Add(u);
                });
                tv.Text = "Agregao";
            }
        }
    }
}

