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
    [Activity(Label = "Mijn Profiel")]
    public class Profiel : MainActivity
    {
        private Button Bewerk;
        private TextView Naam;
        private TextView Email;
        private TextView Bloedtype;
        private List<string> profiel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            RequestWindowFeature(WindowFeatures.NoTitle);
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.profielscherm);

            Bewerk = FindViewById<Button>(Resource.Id.bewerk);

            Naam = FindViewById<TextView>(Resource.Id.naam);
            Email = FindViewById<TextView>(Resource.Id.email);
            Bloedtype = FindViewById<TextView>(Resource.Id.bloedgroep);

            profiel = DBconnect.Get_Profile(EmailLogin);

            Naam.Text = "Naam: " + profiel[0] + " " + profiel[1];
            Email.Text = "Email: " + profiel[2];
            Bloedtype.Text = "Bloedtype: " + profiel[3];

        }
    }
}