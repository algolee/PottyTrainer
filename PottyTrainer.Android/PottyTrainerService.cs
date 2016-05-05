using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;
using PottyTrainer.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;

namespace PottyTrainer.Android
{
    [Service]
    public class PottyTrainerService : Service
    {
        private const string API_URL = "http://localhost:7375/api/pottytrainer";
        private RestClient _RestClient;
        private IBinder _Binder;

        public event EventHandler<PeePooEventSavedArgs> SavedCompleted = delegate { };


        private void RaiseSavedCompleted(int eventId, bool success = true, string errorMessage = "")
        {
            SavedCompleted?.Invoke(this, new PeePooEventSavedArgs(eventId) { ErrorMessage = errorMessage, IsSuccess = success });
        }

        public override IBinder OnBind(Intent intent)
        {
            _Binder = new DataServiceBinder(this);
            return _Binder;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {

            Log.Debug("PottyTrainerService", " Service Started");
            _RestClient = new RestClient(API_URL);
            return StartCommandResult.Sticky;


        }

        public void SaveEvent(PeePooEvent evt)
        {
            var request = new RestRequest("events", Method.POST)
            {
                UseDefaultCredentials = true,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()

            };
            request.AddBody(evt);
            var response = _RestClient.Execute(request);
            var eventid = JsonConvert.DeserializeObject<int>(response.Content);
            RaiseSavedCompleted(eventid, response.ResponseStatus != ResponseStatus.Error, response.ErrorMessage);


        }

        public void DeleteEvent(long id)
        {
            throw new NotImplementedException();
        }

        public void GetEvent(long id)
        {
            throw new NotImplementedException();
        }

        public List<PeePooEvent> GetEvents()
        {
            throw new NotImplementedException();
        }
    }

    public class PeePooEventSavedArgs : EventArgs
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public int EventId { get; set; }

        public PeePooEventSavedArgs()
        {
        }

        public PeePooEventSavedArgs(int id)
        {
            EventId = id;
        }
    }

    public class DataServiceBinder : Binder
    {
        public PottyTrainerService PottyTrainerService { get; set; }

        public bool IsBound { get; set; }

        public DataServiceBinder(PottyTrainerService svc)
        {
            PottyTrainerService = svc;
        }

    }



    public class PottyTrainerServiceConnection : Java.Lang.Object, IServiceConnection
    {
        public EventHandler<ServiceConnectedEventArgs> ServiceConnected = delegate { };
        public DataServiceBinder DataServiceBinder { get; private set; }
        public PottyTrainerServiceConnection(DataServiceBinder binder)
        {
            if (binder == null) return;
            DataServiceBinder = binder;

        }

        public PottyTrainerServiceConnection()
        {
            DataServiceBinder = new DataServiceBinder(new PottyTrainerService());
        }
        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var binder = service as DataServiceBinder;
            if (binder == null) return;
            DataServiceBinder = binder;
            DataServiceBinder.IsBound = true;
            RaiseServiceConnected();


        }

        private void RaiseServiceConnected()
        {
            ServiceConnected(this, new ServiceConnectedEventArgs(DataServiceBinder));
        }
        public void OnServiceDisconnected(ComponentName name)
        {
            DataServiceBinder.IsBound = false;
        }
    }

    public class ServiceConnectedEventArgs : EventArgs
    {
        public IBinder Binder { get; set; }

        public ServiceConnectedEventArgs(IBinder binder)
        {
            Binder = binder;
        }
    }
}