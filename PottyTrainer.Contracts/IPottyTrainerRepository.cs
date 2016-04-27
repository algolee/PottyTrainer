using System.Collections.Generic;

namespace PottyTrainer.Contracts
{
    public interface IPottyTrainerRepository
    {
        long SaveEvent(PeePooEvent evt);
        bool DeleteEvent(long id);
        PeePooEvent GetEvent(long id);
        List<PeePooEvent> GetEvents();


    }
}
