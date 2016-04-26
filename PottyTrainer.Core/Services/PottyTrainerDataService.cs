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
        public long SaveEvent(PeePooEvent evt)
        {
            return _repository.SaveEvent(evt);
        }

        public bool DeleteEvent(long id)
        {
            return _repository.DeleteEvent(id);
        }

        public PeePooEvent GetEvent(long id)
        {
            return _repository.GetEvent(id);
        }

        public List<PeePooEvent> GetEvents()
        {
            return _repository.GetEvents();

        }
    }
}