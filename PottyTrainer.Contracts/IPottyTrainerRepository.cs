using System.Collections.Generic;

namespace PottyTrainer.Contracts
{
    public interface IPottyTrainerRepository
    {
        long SaveEvent(PeePooEvent evt);
        bool DeleteEvent(int id);
        PeePooEvent GetEvent(int id);
        List<PeePooEvent> GetEvents();


    }
}
