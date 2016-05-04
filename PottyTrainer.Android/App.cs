using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PottyTrainer.Android
{
    public class App
    {
        private static App _Instance;
        private static object _lock = new object();
        private static PottyTrainerServiceConnection _ServiceConnection;

        public static App Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (_lock)
                    {
                        if (_Instance == null)
                            _Instance = new App();
                    }
                }
                return _Instance;
            }
        }

        protected App()
        {
            _ServiceConnection = new PottyTrainerServiceConnection(null);
            _ServiceConnection.
            
        }

        public static void StartServices()
        {
            //https://developer.xamarin.com/guides/android/application_fundamentals/backgrounding/part_3_android_backgrounding_walkthrough/
            new Task(() =>  Android.App.Application.Context.StartService(
                new Intent(AndroidEnvironment.)
                ));
        }

        public static void StopServices()
        {
            
        }
    }
}