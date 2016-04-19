using Android.App;
using Android.OS;
using Android.Widget;
using System;

namespace PottyTrainer.Android
{
    [Activity(Label = "Potty Trainer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var btnPee = FindViewById<Button>(Resource.Id.btnPee);
            var btnPoo = FindViewById<Button>(Resource.Id.btnPoo);
            var btnBoth = FindViewById<Button>(Resource.Id.btnBoth);

            btnPee.Click += ButtonClick;
            btnPoo.Click += ButtonClick;
            btnBoth.Click += ButtonClick;
        }

        private void ButtonClick(object sender, EventArgs eventArgs)
        {
            var btn = sender as Button;
            if (btn == null) return;


        }
    }
}

