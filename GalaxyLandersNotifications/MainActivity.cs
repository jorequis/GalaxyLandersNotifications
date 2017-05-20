﻿using Android.App;
using Android.Widget;
using Android.OS;
using Realms;

namespace GalaxyLandersNotifications
{
    [Activity(Label = "GalaxyLandersNotifications", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            TextView tv = FindViewById<TextView>(Resource.Id.textView1);
        }
    }
}

