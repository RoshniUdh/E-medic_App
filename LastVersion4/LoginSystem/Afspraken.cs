using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace LoginSystem
{
    [Activity(Label = "Afspraken")]
    public class Afspraken : MainActivity
    {
        private ListView listnames;
        private Button mbtnAfspraken;
        private List<string> itemlist;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.afspraken);
            listnames = FindViewById<ListView>(Resource.Id.ListviewAfspraken);
            itemlist = DBconnect.GetList("select * from Afspraken where email = @email", 0, EmailLogin);

            ArrayAdapter<string> new_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, itemlist);
            listnames.Adapter = new_adapter;

            mbtnAfspraken = FindViewById<Button>(Resource.Id.afspraken);

            mbtnAfspraken.Click += (object sender, EventArgs e) => 
            {
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                dialog_afspraken aardappels = new dialog_afspraken();
                aardappels.Show(transaction, "dialog afspraken");

                aardappels.mOnafspraakConplete += Aardappels_mOnafspraakConplete;
            };
        }

      

        private void Aardappels_mOnafspraakConplete(object sender, OnAfspraakArgs e)
        {
            string Email = e.Email;
            string Date = e.Date;
            string Time = e.Tijd;
            string beschrijving = e.Beschrijving;

            //DataTable date = DBconnect.GrabData("select * from afspraken");

            DBconnect.PushDataAfspraken(Time, Date, beschrijving, Email);
        }
    }
}