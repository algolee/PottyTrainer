using Android.App;
using Android.Content;
using Android.OS;
using System;
using Android.Util;

namespace PottyTrainer.Android
{
    [Service]
    public class PottyTrainerService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            Log.Debug("PottyTrainerService"," Service Started");
            return base.OnStartCommand(intent, flags, startId);

        }
    }
}