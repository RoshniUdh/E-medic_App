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
using static Android.Widget.AdapterView;

namespace LoginSystem
{
    [Activity(Label = "Voorraad")]
    public class Voorraad : MainActivity
    {
        private Button mRegisterMedic;
        //private Button mbtnMelding;
        MainActivity a = new MainActivity();
        List<string> itemlist;
        private ListView listnames;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.voorraadScherm);

            mRegisterMedic = FindViewById<Button>(Resource.Id.Medicijn_Toe);
            //mbtnMelding = FindViewById<Button>(Resource.Id.button1);
            listnames = FindViewById<ListView>(Resource.Id.ListviewMED);
            itemlist = DBconnect.GetList("Select * from Medicijn where email = @email", 0, EmailLogin);




            mRegisterMedic.Click += MRegisterMedic_Click;
            ArrayAdapter<string> new_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, itemlist);
            listnames.Adapter = new_adapter;
            listnames.InvalidateViews();
            listnames.ItemClick += mbtnMelding_Click;
        }

        private void mbtnMelding_Click(object sender, ItemClickEventArgs e2)
        {
            //shared prefereces we can use
            String name = (String)listnames.GetItemAtPosition(e2.Position);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("medicineName", name.Trim());
            editor.Apply();
            var profieltje = new Intent(this, typeof(Melding));
            this.StartActivity(profieltje);
        }

        private void MRegisterMedic_Click(object sender, EventArgs e)
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            dialog_medicijnen medicijnenscherm = new dialog_medicijnen();
            medicijnenscherm.Show(transaction, "medicijnen fragment");

            medicijnenscherm.mOnMedicComplete += medicijnenscherm_mOnMedicComplete;
        }

        void medicijnenscherm_mOnMedicComplete(object sender, OnMedicEventArgs e)
        {   /// Hier moegt nog aan gewerkt worden
            string Email = Checked_email.ElementAt(0);
            DBconnect.PushDataMedicijn(e.Medicijn,0,e.Bijsluiter,0,"",Email);
        }
    }
}