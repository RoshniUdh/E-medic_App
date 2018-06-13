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
    public class OnMedicEventArgs : EventArgs
    {
        private string mMedicijn;
        private string mVoorraad;
        private string mBijsluiter;
        private string mDosering;


        public string Medicijn
        {
            get { return mMedicijn; }
            set { mMedicijn = value; }
        }

        public string Voorraad
        {
            get { return mVoorraad; }
            set { mVoorraad = value; }
        }

        public string Bijsluiter
        {
            get { return mBijsluiter; }
            set { mBijsluiter = value; }
        }

        public string Dosering
        {
            get { return mDosering; }
            set { mDosering = value; }
        }

        public OnMedicEventArgs(string name, string voorraad, string bijsluiter, string dosering) : base()
        {
            Medicijn = name;
            Voorraad = voorraad;
            Bijsluiter = bijsluiter;
            Dosering = dosering;
        }
    }
    class dialog_medicijnen : DialogFragment
    {
        private EditText mTxtMedicijn;
        private EditText mTxtVoorraad;
        private EditText mTxtDosering;
        private EditText mTxtBijsluiter;
        private Button mUpload;

        public event EventHandler<OnMedicEventArgs> mOnMedicComplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_medicijn, container, false);

            mTxtMedicijn = view.FindViewById<EditText>(Resource.Id.txtMedicijn);
            mTxtVoorraad = view.FindViewById<EditText>(Resource.Id.txtVoorraad);
            mTxtDosering = view.FindViewById<EditText>(Resource.Id.txtDosering);
            mTxtBijsluiter = view.FindViewById<EditText>(Resource.Id.txtBijsluiter);
            mUpload = view.FindViewById<Button>(Resource.Id.uploadbutton);

            mUpload.Click += Upload_Click;

            return view;
        }

        void Upload_Click(object sender, EventArgs e)
        {
            //User has clicked the button
            mOnMedicComplete.Invoke(this, new OnMedicEventArgs(mTxtMedicijn.Text, mTxtVoorraad.Text, mTxtDosering.Text, mTxtBijsluiter.Text));
            this.Dismiss();
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("medicijn", mTxtMedicijn.Text);
            editor.Apply();
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle); //Sets the title bar to invisible
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; //set the animation
        }
    }
}