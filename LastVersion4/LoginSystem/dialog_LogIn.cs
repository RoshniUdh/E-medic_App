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
    public class OnLogInEventArgs : EventArgs
    {
        private string mEmail;
        private string mPassword;


        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }

        public string Password
        {
            get { return mPassword; }
            set { mPassword = value; }
        }


        public OnLogInEventArgs(string email, string password) : base()
        {
            Email = email;
            Password = password;
        }
    }
    class Dialog_Login : DialogFragment
    {
        private EditText mTxtEmail2;
        private EditText mTxtPassword2;
        private Button BtnLogIn;

        public event EventHandler<OnLogInEventArgs> mOnLogInComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_login_in, container, false);

            mTxtEmail2 = view.FindViewById<EditText>(Resource.Id.txtEmail2);
            mTxtPassword2 = view.FindViewById<EditText>(Resource.Id.txtPassword2);
            BtnLogIn = view.FindViewById<Button>(Resource.Id.BtnLogIn);

            BtnLogIn.Click += BtnLogIn_Click;

            return view;
        }

        void BtnLogIn_Click(object sender, EventArgs e)
        {
            //User has clicked the button
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("email", mTxtEmail2.Text);
            editor.Apply();
            mOnLogInComplete.Invoke(this, new OnLogInEventArgs(mTxtEmail2.Text, mTxtPassword2.Text));
            this.Dismiss();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    }
}