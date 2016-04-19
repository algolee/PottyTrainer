using PottyTrainer.Core.Models;
using PottyTrainer.Core.Repository;
using System.Collections.Generic;

namespace PottyTrainer.Core.Services
{
    public class PottyTrainerDataService
    {
        private static IPottyTrainerRepository _repository;

        public PottyTrainerDataService(IPottyTrainerRepository repository)
        {
            _repository = repository;
        }
        public bool SaveEvent(PeePooEvent evt)
        {
            return true;
        }

        public bool DeleteEvent(long id)
        {
            return true;
        }

        public PeePooEvent GetEvent(long id)
        {
            return null;
        }

        public List<PeePooEvent> GetEvents()
        {
            return new List<PeePooEvent>();
        }
    }
}