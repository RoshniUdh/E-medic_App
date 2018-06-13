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
using Android.Media;
//using Android.Support.V7.App;
using String = System.String;
using NotificationCompat = Android.Support.V4.App.NotificationCompat;
using TaskStackBuilder = Android.Support.V4.App.TaskStackBuilder;
using System.Data;
using System.Data.SqlClient;
using Android.Preferences;
using Java.Interop;

namespace LoginSystem
{
    [Activity(Label = "Melding")]
    public class Melding : MainActivity
    {
        private static readonly int ButtonClickNotificationId = 1000;

        private int count = 1;
        MediaPlayer player1;
        MediaPlayer player2;
        MediaPlayer player3;
        public static MediaPlayer mPlayer;
        private EditText mTxtTime;
        RadioButton radioButton1;
        RadioButton radioButton2;
        RadioButton radioButton3;
        Intent intent;
        public int ringtoneid;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Melding);

            intent = new Intent(this, typeof(NotificationService));
            StartService(intent);

            mTxtTime = FindViewById<EditText>(Resource.Id.textView2);
            radioButton1 = FindViewById<RadioButton>(Resource.Id.radioButton1);
            radioButton2 = FindViewById<RadioButton>(Resource.Id.radioButton2);
            radioButton3 = FindViewById<RadioButton>(Resource.Id.radioButton3);
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            ImageButton imageButton1 = FindViewById<ImageButton>(Resource.Id.imageButton1);
            ImageButton imageButton2 = FindViewById<ImageButton>(Resource.Id.imageButton2);
            ImageButton imageButton3 = FindViewById<ImageButton>(Resource.Id.imageButton3);
            ImageButton imageButton4 = FindViewById<ImageButton>(Resource.Id.imageButton4);
            ImageButton imageButton5 = FindViewById<ImageButton>(Resource.Id.imageButton5);
            ImageButton imageButton6 = FindViewById<ImageButton>(Resource.Id.imageButton6);


            imageButton1.Click += ImageClick1;
            imageButton2.Click += ImageClick2;
            imageButton3.Click += ImageClick3;
            imageButton4.Click += ImageClickStop;
            imageButton5.Click += ImageClickStop;
            imageButton6.Click += ImageClickStop;
            mTxtTime.Click += mTxtTime_Click;
            DataTable data = DBconnect.GrabData("select * from  Melding");


            // Set our view from the "main" layout resource
            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += ButtonOnClick;



            void ButtonOnClick(object sender, EventArgs eventArgs)
            {
                // Database update
                // We have to update or insert new time and date

                // full control is in database => if we update databse, we get notification in app for the updated time
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
                String email = prefs.GetString("email", "example@android.com");
                String medicijn = prefs.GetString("medicineName", "");
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt("ringtoneidnr", ringtoneid);
                editor.Apply();

                RadioButton rb = null;
                if (radioButton1.Checked == true)
                {
                    rb = radioButton1;
                    ringtoneid = 1;
                }
                else if (radioButton2.Checked == true)
                {
                    rb = radioButton2;
                    ringtoneid = 2;
                }
                else if (radioButton3.Checked == true)
                {
                    rb = radioButton3;
                    ringtoneid = 3;
                }

                DBconnect.PushDataMelding(mTxtTime.Text, email, ringtoneid, medicijn);

                StopService(new Intent(this, typeof(NotificationService)));
                StartService(new Intent(this, typeof(NotificationService)));
                // DBconnect insert values in Melding table




                // Pass the current button press count value to the next activity:
                //Bundle valuesForActivity = new Bundle();
                //valuesForActivity.PutInt("count", count);
                //valuesForActivity.PutString("name", "Rose");

                //ISharedPreferencesEditor editor = prefs.Edit();
                //editor.PutInt("count", count);
                //editor.PutString("name", "Rose");
                //editor.Apply();


                // When the user clicks the notification, SecondActivity will start up.
                //Intent resultIntent = new Intent(this, typeof(MeldingErna));

                //// Pass some values to SecondActivity:
                //// resultIntent.PutExtras(valuesForActivity);

                //// Construct a back stack for cross-task navigation:
                //TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
                //stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MeldingErna)));
                //stackBuilder.AddNextIntent(resultIntent);

                //// Create the PendingIntent with the back stack:            
                //PendingIntent resultPendingIntent =
                //    stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);


                //long[] vibrate = new long[] { 1000, 1000, 1000, 1000, 1000 };


                //// Build the notification: This is required to show notification
                //NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                //    .SetAutoCancel(true)                    // Dismiss from the notif. area when clicked
                //    .SetContentIntent(resultPendingIntent)  // Start 2nd activity when the intent is clicked.
                //    .SetContentTitle("Button Clicked")      // Set its title
                //    .SetVibrate(vibrate)
                //    .SetNumber(count)                       // Display the count in the Content Info
                //    .SetSmallIcon(Resource.Drawable.Icon)  // Display this icon
                //    .SetContentText(String.Format(
                //        "The button has been clicked {0} times.", count)); // The message to display.








                //// Finally, publish the notification:
                //NotificationManager notificationManager =
                //            (NotificationManager)GetSystemService(Context.NotificationService);
                //notificationManager.Notify(ButtonClickNotificationId, builder.Build());

                // Increment the button press count:
                //count++;

            }




            //void onDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
            //{
            //    // TODO Auto-generated method stub
            //    int month = e.Month + 1;
            //    mTxtDate.Text = month.ToString() + "/" + e.DayOfMonth.ToString() + "/" + e.Year.ToString();

            //}

            void onTimeSet(object sender, TimePickerDialog.TimeSetEventArgs e)
            {
                // TODO Auto-generated method stub
                int hour = e.HourOfDay;
                int minutes = e.Minute;

                String aTime = hour.ToString() + ":" + minutes.ToString();
                mTxtTime.Text = aTime;

            }


            //void mTxtDate_Click(object sender, EventArgs e)
            //{
            //    //User has clicked the edit text
            //    DateTime today = DateTime.Today;
            //    DatePickerDialog dialog = new DatePickerDialog(this, onDateSet, today.Year, today.Month - 1, today.Day);
            //    dialog.Show();

            //}

            void mTxtTime_Click(object sender, EventArgs e)
            {
                //User has clicked the edit text
                DateTime today = DateTime.Today;
                TimePickerDialog dialog = new TimePickerDialog(this, onTimeSet, today.Hour, today.Minute, true);
                dialog.Show();

            }

            void ImageClick1(object sender, EventArgs e)
            {

                //player1 = MediaPlayer.Create(this, Resource.Raw.ialarm);
                //player1.Start();
                //player1.
                if(mPlayer!=null && mPlayer.IsPlaying)
                {
                    mPlayer.Stop();
                }           
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                radioButton3.Checked = false;
                mPlayer = MediaPlayer.Create(this, Resource.Raw.ialarm);
                mPlayer.Start();
            }

            void ImageClick2(object sender, EventArgs e)
            {
                //player2 = MediaPlayer.Create(this, Resource.Raw.radar);
                //player2.Start();
                if (mPlayer != null && mPlayer.IsPlaying)
                {
                    mPlayer.Stop();
                }
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                radioButton3.Checked = false;
                mPlayer = MediaPlayer.Create(this, Resource.Raw.radar);
                mPlayer.Start();
            }

            void ImageClick3(object sender, EventArgs e)
            {
                //player3 = MediaPlayer.Create(this, Resource.Raw.sunalarm);
                //player3.Start();
                if (mPlayer != null && mPlayer.IsPlaying)
                {
                    mPlayer.Stop();
                }
                radioButton1.Checked = false;
                radioButton2.Checked = false;
                radioButton3.Checked = true;
                mPlayer = MediaPlayer.Create(this, Resource.Raw.sunalarm);
                mPlayer.Start();
            }

            void ImageClickStop(object sender, EventArgs e)
            {
                //player1 = MediaPlayer.Create(this, Resource.Raw.ialarm);
                //player2 = MediaPlayer.Create(this, Resource.Raw.radar);
                //player3 = MediaPlayer.Create(this, Resource.Raw.sunalarm);

                ////player.Start();
                //if ((player1.IsPlaying == false) || (player2.IsPlaying == false) || (player3.IsPlaying == false))
                //{
                //    player1.Stop();
                //}
                //else if ((player1.IsPlaying == true) || (player2.IsPlaying == true) || (player3.IsPlaying == true))
                //{
                //    player1.Stop();
                //}
                //else
                ////player1.Stop();
                ////player1.Stop();
                ////player2.Stop();
                ////player3.Stop(); 

                //if (radioButton1.Checked == true)
                //{
                //    player1.Stop();

                //}
                //else if (radioButton2.Checked == true)
                //{
                //      player1.Stop();

                //}
                //else if (radioButton3.Checked == true)
                //{
                //      player1.Stop();

                //}

                if (mPlayer != null && mPlayer.IsPlaying)
                {
                    mPlayer.Stop();
                }

            }

        }
    }
}