using PottyTrainer.Core.Models;
using System.Collections.Generic;

namespace PottyTrainer.Core.Repository
{
    public interface IPottyTrainerRepository
    {
        bool SaveEvent(PeePooEvent evt);
        bool DeleteEvent(long id);
        PeePooEvent GetEvent(long id);
        List<PeePooEvent> GetEvents();


    }
}
