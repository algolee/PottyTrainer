using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.Linq;
using System.Collections.Generic;
using PottyTrainer.Core.Models;
using PottyTrainer.Core.Repository;
using PottyTrainer.Core.Services;

namespace PottyTrainer.Android
{
    [Activity(Label = "Potty Trainer", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private PottyTrainerDataService _DataService;

        public MainActivity()
        {
            
            _DataService = new PottyTrainerDataService(new TestRepository());
        }
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
            var tag = btn.Tag.ToString();

            var evnt = new PeePooEvent {EventWhen = DateTime.Now};
            if (tag.Equals("pee", StringComparison.CurrentCultureIgnoreCase))
                evnt.EventType = EventType.Pee;
            else if (tag.Equals("poo", StringComparison.CurrentCultureIgnoreCase))
                evnt.EventType = EventType.Poo;
            else evnt.EventType = EventType.Both;
            _DataService.SaveEvent(evnt);
        }
    }

    public class TestRepository : IPottyTrainerRepository
    {
        private IDictionary<long, PeePooEvent> _Store= new Dictionary<long, PeePooEvent>();
        private long _NextId = 0;
        public bool SaveEvent(PeePooEvent evt)
        {
            try
            {
                _NextId++;
                _Store.Add(_NextId, evt);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteEvent(long id)
        {
            throw new NotImplementedException();
        }

        public PeePooEvent GetEvent(long id)
        {
            throw new NotImplementedException();
        }

        public List<PeePooEvent> GetEvents()
        {
            throw new NotImplementedException();
        }
    }


}

