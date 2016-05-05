using Android.App;
using Android.Content;
using Android.Runtime;
using System;
using System.Threading.Tasks;

namespace PottyTrainer.Android
{
    [Application]
    public class PottyTrainnerApp : Application
    {
        public PottyTrainnerApp(IntPtr p, JniHandleOwnership q) : base(p, q)
        { }
        public override void OnCreate()
        {
            AppUtils.Instance.StartBackgroundServices();
        }
    }

    public sealed class AppUtils
    {
        private static AppUtils _Instance;
        private static readonly object _lock = new object();
        private static PottyTrainerServiceConnection _ServiceConnection;

        public EventHandler<ServiceConnectedEventArgs> PottyTrainerServiceConnected = delegate { };

        public PottyTrainerService PottyTrainerService => _ServiceConnection.DataServiceBinder.PottyTrainerService;

        public static AppUtils Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_lock)
                    {
                        if (_Instance == null)
                            _Instance = new AppUtils();
                    }
                    return _Instance;
                }
                return _Instance;
            }
        }

        private AppUtils()
        {
            _ServiceConnection = new PottyTrainerServiceConnection(null);
            _ServiceConnection.ServiceConnected += (sender, args) =>
            {
                PottyTrainerServiceConnected(this, args);
            };

        }

        public void StartBackgroundServices()
        {
            new Task(() =>
            {
                Application.Context.StartService(new Intent(Application.Context,
                    typeof(PottyTrainerService)));
                var serviceIntent = new Intent(Application.Context, typeof(PottyTrainerService));
                Application.Context.BindService(serviceIntent, _ServiceConnection, Bind.AutoCreate);
            }).Start();
        }

        public void StopBackgroundServices()
        {
            if (_ServiceConnection != null)
                Application.Context.UnbindService(_ServiceConnection);

        }


    }
}