using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using PottyTrainer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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
            //var progressDialog = ProgressDialog.Show(this, "Please wait...", "Saving info...", true);

            var btn = sender as Button;
            if (btn == null) return;
            var tag = btn.Tag.ToString();

            var evnt = new PeePooEvent { EventWhen = DateTime.Now };
            if (tag.Equals("pee", StringComparison.CurrentCultureIgnoreCase))
                evnt.EventType = EventType.Pee;
            else if (tag.Equals("poo", StringComparison.CurrentCultureIgnoreCase))
                evnt.EventType = EventType.Poo;
            else evnt.EventType = EventType.Both;
            //progressDialog.Show();
            //new Task(() =>
            //{
            AppUtils.Instance.PottyTrainerService.SaveEvent(evnt, args =>
            {
                RunOnUiThread(() =>
                {
                        // progressDialog.Hide();
                        if (args.IsSuccess)
                        LoadNextActivity(args.EventId);
                    else
                        Toast.MakeText(this, args.ErrorMessage, ToastLength.Long).Show();



                });

            });
            //}).Start(TaskScheduler.Current);

        }

        private void LoadNextActivity(long id)
        {
            var intent = new Intent(this, typeof(DetailsActivity));
            intent.PutExtra("eventId", id);
            StartActivityForResult(intent, 200);
        }
    }

    public class TestRepository : IPottyTrainerRepository
    {
        private IDictionary<long, PeePooEvent> _Store = new Dictionary<long, PeePooEvent>();
        private int _NextId = 0;
        public long SaveEvent(PeePooEvent evt)
        {
            try
            {
                if (evt.Id <= 0)
                {
                    _NextId++;
                    evt.Id = _NextId;
                    _Store.Add(_NextId, evt);
                }
                else
                {
                    _Store[evt.Id] = evt;

                }
                return evt.Id;
            }
            catch (Exception)
            {

                return -1;
            }
        }

        public bool DeleteEvent(int id)
        {
            return _Store.Remove(id);

        }

        public PeePooEvent GetEvent(int id)
        {
            return _Store[id];
        }

        public List<PeePooEvent> GetEvents()
        {
            return _Store.Values.ToList();

        }
    }


}

