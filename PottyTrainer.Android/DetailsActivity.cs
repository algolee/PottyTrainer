
using Android.App;
using Android.OS;
using Android.Widget;
using PottyTrainer.Contracts;
using System;

namespace PottyTrainer.Android
{
    [Activity(Label = "Details")]
    public class DetailsActivity : Activity
    {
        private PeePooEvent _CurrentEvent;
        private Button _BtnDate;
        private Button _BtnTime;
        // private PottyTrainerDataService _DataService;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.DetailsView);
            _BtnDate = FindViewById<Button>(Resource.Id.btnDate);
            _BtnTime = FindViewById<Button>(Resource.Id.btnTime);
            _BtnDate.Click += BtnDateOnClick;
            _BtnTime.Click += BtnTimeOnClick;
            var btnDone = FindViewById<Button>(Resource.Id.btnDone);
            btnDone.Click += SaveEvent;
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            //_DataService.SaveEvent(_CurrentEvent);
        }

        private void BtnDateOnClick(object sender, EventArgs eventArgs)
        {

        }

        private void BtnTimeOnClick(object sender, EventArgs eventArgs)
        {

            var frag = new DatePickerDialog(this, OnTimeSelected, 2016, 5, 5);
            frag.Show();
        }

        private void OnTimeSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {

        }
    }
}