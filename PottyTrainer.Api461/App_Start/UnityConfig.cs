using Microsoft.Practices.Unity;
using PottyTrainer.DataModel;
using PottyTrainer.DataSource;
using System.Web.Http;
using Unity.WebApi;

namespace PottyTrainer.Api461
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IPottyTrainerRepository, PottyTrainerRepository>(new PerThreadLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}