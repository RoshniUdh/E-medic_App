using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using Android.Preferences;
using System.Data;
using Android.Util;

namespace LoginSystem
{
    [Service(Enabled = true, Exported = true)]
    class NotificationService : Service
    {
        private PendingIntent pendingIntent;
        public NotificationService()
        {
        }

    public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            Log.Debug("Information","Notification service stopped");
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Log.Debug("Information", "Notification service Started");
            Calendar calendar = Calendar.Instance;
            AlarmManager alarmManager = (AlarmManager)GetSystemService(AlarmService);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            string email = prefs.GetString("email", "");
            string medicine = prefs.GetString("medicineName", "");
            string query = "select [tijd],[ringtoneid] from Melding where email = '"+email+"' and medicijn = '"+ medicine + "'; ";

            DataTable Data = DBconnect.GrabData(query);
            int idIndex = 0;
            foreach(DataRow row in Data.Rows)
            {
                TimeSpan time = (TimeSpan)row[0];
                int ringToneId = (int)row[1];
                calendar.Set(Calendar.HourOfDay, time.Hours);
                calendar.Set(Calendar.Minute, time.Minutes);
                calendar.Set(Calendar.Second, time.Seconds);
                Intent myIntent = new Intent(this, typeof(NotificationReceiver));
                pendingIntent = PendingIntent.GetBroadcast(this, idIndex, myIntent, 0);
                alarmManager.SetRepeating(AlarmType.RtcWakeup, calendar.TimeInMillis, 24*60*60*1000, pendingIntent); // after a day
                idIndex++;
                //24 * 60 * 60 *
            }
            //calendar.set(Calendar.MONTH, 8);
            //calendar.set(Calendar.YEAR, 2016)
            //calendar.set(Calendar.DAY_OF_MONTH, 18);
            

        //    calendar.Set(Calendar.HourOfDay, 0);
        //    calendar.Set(Calendar.Minute, 8);
        //    calendar.Set(Calendar.Second, 0);
        //    //
        //    Intent myIntent = new Intent(this, typeof(NotificationReceiver));
        //pendingIntent = PendingIntent.GetBroadcast(this, 0, myIntent,0);

        //AlarmManager alarmManager = (AlarmManager) GetSystemService(AlarmService);
        //alarmManager.SetRepeating(AlarmType.Rtc, calendar.TimeInMillis,10000, pendingIntent);
            return base.OnStartCommand(intent, flags, startId);
        }
    }
}