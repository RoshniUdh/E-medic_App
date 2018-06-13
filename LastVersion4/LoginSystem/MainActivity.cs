using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using Android.Support.V4.Widget;
using Xamarin.Android;

namespace LoginSystem
{
    [Activity(Label = "E-Medic", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button mBtnLogIn;
        private Button mBtnSignUp;
        private ProgressBar mProgressBar;
        public static List<string> Checked_email = new List<string>();
        public static string EmailLogin;
        
       

        protected override void OnCreate(Bundle bundle)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(bundle);

            

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mBtnSignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            mProgressBar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            mBtnLogIn = FindViewById<Button>(Resource.Id.btnSignIn);
            

            mBtnSignUp.Click += (object sender, EventArgs args) =>
            {
                //Pull up dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_SignUp signUpDialog = new dialog_SignUp();
                signUpDialog.Show(transaction, "dialog fragment");

                signUpDialog.mOnSignUpComplete += SignUpDialog_mOnSignUpComplete;
            };

            mBtnLogIn.Click += (object sender, EventArgs args) =>
            {
                FragmentTransaction transaction2 = FragmentManager.BeginTransaction();
                Dialog_Login aardappel = new Dialog_Login();
                aardappel.Show(transaction2, "potato fragment");

                aardappel.mOnLogInComplete += LogInDialog_mOnLogInComplete;

            };            
        }

        void LogInDialog_mOnLogInComplete(object sender, OnLogInEventArgs e)
        {
            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();

            DataTable data = DBconnect.GrabData("select * from  Gebruiker");

            var emailList = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                var EmailObject = row[1];
                if (EmailObject != null)
                {
                    emailList.Add(EmailObject.ToString());
                }
            }
            // hier word gecheckt of de email bestaat zo niet dan krijg je een error te zien
            bool available = DBconnect.CheckForAvailableEmail(emailList, e.Email);
            if (!available)
            {       //check hier of wachtwoord klopt met email
                if (DBconnect.PullDataLoginAndCompare(e.Email, e.Password))
                {
                    EmailLogin = e.Email;
                    Checked_email.Add(e.Email);
                    Intent home = new Intent(this, typeof(Homescreen));
                    this.StartActivity(home);
                    Toast.MakeText(this, "Inloggen Gelukt!", ToastLength.Long).Show();
                   
                }



                else
                {
                    AlertDialog.Builder WachtwoordDialog = new AlertDialog.Builder(this);
                    WachtwoordDialog.SetTitle("Error");
                    WachtwoordDialog.SetMessage("Email of Wachtwoord, bestaat niet");
                    WachtwoordDialog.SetNeutralButton("OK", delegate
                    {
                        FragmentTransaction transaction2 = FragmentManager.BeginTransaction();
                        Dialog_Login aardappel = new Dialog_Login();
                        aardappel.Show(transaction2, "potato fragment");

                        aardappel.mOnLogInComplete += LogInDialog_mOnLogInComplete;

                    });
                    WachtwoordDialog.Show();


                }
            }
            // dit is de error die je ziet als je email niet klopt
            else
            {
                AlertDialog.Builder WachtwoordDialog = new AlertDialog.Builder(this);
                WachtwoordDialog.SetTitle("Error");
                WachtwoordDialog.SetMessage("Email of Wachtwoord, bestaat niet");
                WachtwoordDialog.SetNeutralButton("OK", delegate
                {
                    FragmentTransaction transaction2 = FragmentManager.BeginTransaction();
                    Dialog_Login aardappel = new Dialog_Login();
                    aardappel.Show(transaction2, "potato fragment");

                    aardappel.mOnLogInComplete += LogInDialog_mOnLogInComplete;

                });
                WachtwoordDialog.Show();
            }
        }

        void SignUpDialog_mOnSignUpComplete(object sender, OnSignUpEventArgs e)
        {

            mProgressBar.Visibility = ViewStates.Visible;
            Thread thread = new Thread(ActLikeARequest);
            thread.Start();

            string FirstName = e.FirstName;
            string Email = e.Email;
            string Password = e.Password;
            string LastName = e.LastName;
            int leeftijd = e.Leeftijd;
            string bloedgroep = e.Bloedtype;

            DataTable data = DBconnect.GrabData("select * from  Gebruiker");

            var emailList = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                var EmailObject = row[1];
                if (EmailObject != null)
                {
                    emailList.Add(EmailObject.ToString());
                }
            }
            bool available = DBconnect.CheckForAvailableEmail(emailList, e.Email);
            if (available)
            {
                DBconnect.PushDataAccount(FirstName, Email, Password, LastName, leeftijd, bloedgroep);
            }
            else
            {

                AlertDialog.Builder alertDialog = new AlertDialog.Builder(this);
                alertDialog.SetTitle("Error");
                alertDialog.SetMessage("Email is al in gebruik");
                alertDialog.SetNeutralButton("OK", delegate
                {
                    alertDialog.Dispose();
                    //Pull up dialog
                    FragmentTransaction transaction = FragmentManager.BeginTransaction();
                    dialog_SignUp signUpDialog = new dialog_SignUp();
                    signUpDialog.Show(transaction, "dialog fragment");

                    signUpDialog.mOnSignUpComplete += SignUpDialog_mOnSignUpComplete;
                });
                alertDialog.Show();

            };
        }

        private void ActLikeARequest()
        {
            Thread.Sleep(3000);

            RunOnUiThread(() => { mProgressBar.Visibility = ViewStates.Invisible; });
            int x = Resource.Animation.slide_right;
        }

        

    }
}

