using System.Collections.Generic;
using System.Threading.Tasks;

namespace PottyTrainer.DataModel
{
    public interface IPottyTrainerRepository
    {
        Task<string> SaveEvent(PeePooEvent evt);
        Task<bool> DeleteEvent(string id);
        PeePooEvent GetEvent(string id);
        List<PeePooEvent> GetEvents();
    }
}
