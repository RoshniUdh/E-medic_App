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

namespace LoginSystem
{
    public class OnSignUpEventArgs : EventArgs
    {
        private string mFirstName;
        private string mEmail;
        private string mPassword;
        private string mLastName;
        private int mLeeftijd;
        private string mBloedtype;


        public string FirstName
        {
            get { return mFirstName; }
            set { mFirstName = value; }
        }

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

        public string LastName
        {
            get { return mLastName; }
            set { mLastName = value; }
        }

        public int Leeftijd
        {
            get { return mLeeftijd; }
            set { mLeeftijd = value; }
        }

        public string Bloedtype
        {
            get { return mBloedtype; }
            set { mBloedtype = value; }
        }


        public OnSignUpEventArgs(string firstName, string email, string password, string lastname, int leeftijd, string bloedtype) : base()
        {
            FirstName = firstName;
            Email = email;
            Password = password;
            LastName = lastname;
            Leeftijd = leeftijd;
            Bloedtype = bloedtype;
        }
    }

    class dialog_SignUp : DialogFragment
    {
        private EditText mTxtFirstName;
        private EditText mTxtEmail;
        private EditText mTxtPassword;
        private EditText mTxtLastName;
        private EditText mTxtLeeftijd;
        private EditText mTxtBloedtype;
        private Button mBtnSignUp;

        public event EventHandler<OnSignUpEventArgs> mOnSignUpComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view2 = inflater.Inflate(Resource.Layout.dialog_sign_up, container, false);

            mTxtFirstName = view2.FindViewById<EditText>(Resource.Id.txtFirstName);
            mTxtEmail = view2.FindViewById<EditText>(Resource.Id.txtEmail);
            mTxtPassword = view2.FindViewById<EditText>(Resource.Id.txtPassword);
            mTxtLastName = view2.FindViewById<EditText>(Resource.Id.txtLastName);
            mTxtLeeftijd = view2.FindViewById<EditText>(Resource.Id.txtLeeftijd);
            mTxtBloedtype = view2.FindViewById<EditText>(Resource.Id.txtBloedtype);
            mBtnSignUp = view2.FindViewById<Button>(Resource.Id.btnDialogEmail);

            mBtnSignUp.Click += mBtnSignUp_Click;

            return view2;
        }

        void mBtnSignUp_Click(object sender, EventArgs e)
        {
           //User has clicked the sign up button
            mOnSignUpComplete.Invoke(this, new OnSignUpEventArgs(mTxtFirstName.Text, mTxtEmail.Text, mTxtPassword.Text, mTxtLastName.Text, 18, mTxtBloedtype.Text));
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