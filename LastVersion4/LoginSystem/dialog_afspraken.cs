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
    public class OnAfspraakArgs : EventArgs
    {
        private string mEmail;
        private string mDate;
        private string mTijd;
        private string mBeschrijving;

        public string Email
        {
            get { return mEmail; }
            set { mEmail = value; }
        }
        public string Date
        {
            get { return mDate; }
            set { mDate = value; }
        }
        public string Tijd
        {
            get { return mTijd; }
            set { mTijd = value; }
        }
        public string Beschrijving
        {
            get { return mBeschrijving; }
            set { mBeschrijving = value; }
        }
        public OnAfspraakArgs(string email, string date, string tijd, string beschrijving)
        {
            Email = email;
            Date = date;
            Tijd = tijd;
            Beschrijving = beschrijving;
        }
    }


    class dialog_afspraken : DialogFragment
    {
        private EditText mtxtEmail3;
        private EditText mtxtDate;
        private EditText mtxtTijd;
        private EditText mtxtBeschrijving;
        private Button BtnAfspraken;

        public event EventHandler<OnAfspraakArgs> mOnafspraakConplete;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.dialog_afspraken, container, false);

            mtxtEmail3 = view.FindViewById<EditText>(Resource.Id.txtEmail3);
            mtxtDate = view.FindViewById<EditText>(Resource.Id.txtDate);
            mtxtTijd = view.FindViewById<EditText>(Resource.Id.txtTijd);
            mtxtBeschrijving = view.FindViewById<EditText>(Resource.Id.txtBeschrijving);

            BtnAfspraken = view.FindViewById<Button>(Resource.Id.btnDialogAfspraken);

            BtnAfspraken.Click += BtnAfspraken_Click;

            return view;
        }

        private void BtnAfspraken_Click(object sender, EventArgs e)
        {
            mOnafspraakConplete.Invoke(this, new OnAfspraakArgs(mtxtEmail3.Text, mtxtDate.Text, mtxtTijd.Text, mtxtBeschrijving.Text));
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);// sets the title to invisable
            base.OnActivityCreated(savedInstanceState);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation; // set the animation
        }
    }
}