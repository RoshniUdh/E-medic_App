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
    [Activity(Label = "Home")]
    public class Homescreen : MainActivity
    {
        private ListView listnames;
        private List<string> itemlist;
        private Button mbtnAfspraken;
        private Button Profile;
        private Button Voorraad;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            //RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Home);
            listnames = FindViewById<ListView>(Resource.Id.Listview);
            itemlist = DBconnect.GetList("Select medicijn from Melding where email = @email ", 0, EmailLogin);
            mbtnAfspraken = FindViewById<Button>(Resource.Id.afspraken);
            Profile = FindViewById<Button>(Resource.Id.user);
            Voorraad = FindViewById<Button>(Resource.Id.supplies);

            mbtnAfspraken.Click += MbtnAfspraken_Click;
            Profile.Click += Profile_Click;
            Voorraad.Click += Voorraad_Click;

            ArrayAdapter<string> new_adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, itemlist);
            listnames.Adapter = new_adapter;
            listnames.InvalidateViews();

        }

        private void Voorraad_Click(object sender, EventArgs e)
        {
            var schermpie = new Intent(this, typeof(Voorraad));
            this.StartActivity(schermpie);
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            var profieltje = new Intent(this, typeof(Profiel));
            this.StartActivity(profieltje);
        }

        private void MbtnAfspraken_Click(object sender, EventArgs e)
        {
            var afspraken = new Intent(this, typeof(Afspraken));
            this.StartActivity(afspraken);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var Inflater = MenuInflater;
            Inflater.Inflate(Resource.Menu.refreshmenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.refresh)
            {
                Toast.MakeText(this, "Refreshing", ToastLength.Short);
                return true;
            }
            return base.OnOptionsItemSelected(item);
                
        }
    }
}