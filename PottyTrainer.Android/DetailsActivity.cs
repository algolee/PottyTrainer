
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

            var cbPee = FindViewById<CheckBox>(Resource.Id.chbPee);
            var cbPoo = FindViewById<CheckBox>(Resource.Id.chbPoo);
            var cbInToilet = FindViewById<CheckBox>(Resource.Id.chbInToilet);
            var eventId = Intent.GetStringExtra("eventId");
            AppUtils.Instance.PottyTrainerService.GetEvent(eventId, e =>
            {
                RunOnUiThread(() =>
                {
                    _BtnDate.Text = e.EventWhen.ToShortDateString();
                    _BtnTime.Text = e.EventWhen.ToShortTimeString();

                    cbPoo.Checked = e.EventType == EventType.Poo || e.EventType == EventType.Both;
                    cbPee.Checked = e.EventType == EventType.Pee || e.EventType == EventType.Both;
                    cbInToilet.Checked = e.InToilet;
                    _CurrentEvent = e;

                });
            });
        }

        private void SaveEvent(object sender, EventArgs e)
        {
            //_DataService.SaveEvent(_CurrentEvent);
        }

        private void BtnDateOnClick(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;
            var frag = new DatePickerDialog(this, OnDateSelected, now.Year, now.Month, now.Day);
            frag.Show();
        }

        private void BtnTimeOnClick(object sender, EventArgs eventArgs)
        {
            var now = DateTime.Now;
            var frag = new TimePickerDialog(this, OnTimeSelected, now.Hour, now.Minute, true);
            frag.Show();

        }

        private void OnTimeSelected(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            _BtnTime.Text = e.HourOfDay + ":" + e.Minute;

        }

        private void OnDateSelected(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _BtnDate.Text = e.Date.ToShortDateString();
        }
    }
}