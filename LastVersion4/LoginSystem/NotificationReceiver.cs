using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Android.Views;
using Android.Widget;
using Android.Preferences;


namespace LoginSystem
{

    [BroadcastReceiver(Enabled = true, Exported = true)]
    class NotificationReceiver : BroadcastReceiver
    {
        private static readonly int ButtonClickNotificationId = 1000;
        int count = 1;

        public override void OnReceive(Context context, Intent intent)
        {
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            
            String medicineName = prefs.GetString("medicineName", "");
            int ringtoneidnr = prefs.GetInt("ringtoneidnr", 1);

           
            
            // Pass the current button press count value to the next activity:
            //Bundle valuesForActivity = new Bundle();
            //valuesForActivity.PutInt("count", count);
            //valuesForActivity.PutString("name", "Rose");

            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutInt("count", count);
            editor.PutString("name", "Rose");
            editor.Apply();

            Intent resultIntent = new Intent(context, typeof(MeldingErna));

            // Pass some values to SecondActivity:
            // resultIntent.PutExtras(valuesForActivity);
            count++;

            // Construct a back stack for cross-task navigation:
            TaskStackBuilder stackBuilder = TaskStackBuilder.Create(context);
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MeldingErna)));
            stackBuilder.AddNextIntent(resultIntent);

            // Create the PendingIntent with the back stack:            
            PendingIntent resultPendingIntent =
                stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);




            long[] vibrate = new long[] { 1000, 1000, 1000, 1000, 1000 };
            //Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + context.PackageName + "/"
            //                    + Resource.Raw.ialarm);

            // Build the notification: This is required to show notification
            NotificationCompat.Builder builder = new NotificationCompat.Builder(context)
                .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                .SetContentTitle("Alarm :" + medicineName)      // Set its title
                .SetVibrate(vibrate)
                //.SetSound(uri)
                .SetNumber(count)                       // Display the count in the Content Info
                .SetSmallIcon(Resource.Drawable.Icon)  // Display this icon
                .SetContentText(String.Format(
                    "The button has been clicked {0} times.", count)); // The message to display.
                                                                       //throw new NotImplementedException();
            if (ringtoneidnr == 1)
            {
                Android.Net.Uri uri = Android.Net.Uri.Parse("android.resource://" + context.PackageName + "/"
                                + Resource.Raw.ialarm);
                builder.SetSound(uri);
            }
            else if (ringtoneidnr == 2)
            {
                Android.Net.Uri seconduri = Android.Net.Uri.Parse("android.resource://" + context.PackageName + "/"
                                + Resource.Raw.radar);
                builder.SetSound(seconduri);
            }
            else if (ringtoneidnr == 3)
            {
                Android.Net.Uri thirduri = Android.Net.Uri.Parse("android.resource://" + context.PackageName + "/"
                                + Resource.Raw.sunalarm);
                builder.SetSound(thirduri);
            }

            NotificationManager notificationManager =
                           (NotificationManager)context.GetSystemService(Context.NotificationService);
            notificationManager.Notify(ButtonClickNotificationId, builder.Build());

        }
    }
}