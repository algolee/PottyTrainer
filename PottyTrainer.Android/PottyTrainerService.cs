using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using PottyTrainer.Contracts;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace PottyTrainer.Android
{
    [Service]
    public class PottyTrainerService : Service
    {
        private const string API_URL = "http://pottytrainerapi.azurewebsites.net/";
        private RestClient _RestClient;
        private IBinder _Binder;

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

        public void SaveEvent(PeePooEvent evt, Action<PeePooEventSavedArgs> callbackAction)
        {

            var request = new RestRequest("events", Method.POST)
            {
                UseDefaultCredentials = true,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddBody(evt);
            _RestClient.ExecuteAsync(request, response =>
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    callbackAction.Invoke(new PeePooEventSavedArgs("") { ErrorMessage = "Unable to save Event", IsSuccess = false });
                }
                else
                {
                    var eventid = response.Content;
                    eventid = eventid.Replace("\"", "").Trim();
                    callbackAction.Invoke(new PeePooEventSavedArgs(eventid) { IsSuccess = true });
                }
            });
        }

        public void DeleteEvent(long id)
        {
            throw new NotImplementedException();
        }

        public void GetEvent(string id, Action<PeePooEvent> callbackAction)
        {
            var request = new RestRequest("events/{id}", Method.GET)
            {
                UseDefaultCredentials = true,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new JsonSerializer()
            };
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("id", id);
            _RestClient.ExecuteAsync<PeePooEvent>(request, response =>
            {
                callbackAction.Invoke(response.StatusCode != HttpStatusCode.OK ? null : response.Data);
            });


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

        public string EventId { get; set; }

        public PeePooEventSavedArgs()
        {
        }

        public PeePooEventSavedArgs(string id)
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