using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginSystem
{
    [Activity(Label = "MeldingErna")]
    public class MeldingErna : Activity
    {

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            // Get the count value passed to us from Melding:
            //int count = Intent.Extras.GetInt("count", -1);
            //String name = Intent.Extras.GetString("name", "");

            // Get the count updated by main activity from preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            int count = prefs.GetInt("count", -1);
            String name = prefs.GetString("name", "");
            String medicinName = prefs.GetString("medicineName", "");

            // No count was passed? Then just return.
            if (count <= 0)
                return;

            // Display the count sent from the first activity:
            SetContentView(Resource.Layout.MeldingErna);
            TextView textView = FindViewById<TextView>(Resource.Id.textView);
            textView.Text = String.Format(
                "Alarm for {0}: {1} clicked the button {2} times in the previous activity.",medicinName, name, count);
            // Create your application here
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Intent i = new Intent();
            i.SetAction("com.inoek.emedic.notification");
            SendBroadcast(i);
        }
    }
}