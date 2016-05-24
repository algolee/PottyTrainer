using System.Collections.Generic;

namespace PottyTrainer.DataModel
{
    public interface IPottyTrainerRepository
    {
        string SaveEvent(PeePooEvent evt);
        bool DeleteEvent(string id);
        PeePooEvent GetEvent(string id);
        List<PeePooEvent> GetEvents();
    }
}
