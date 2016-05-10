using System.Collections.Generic;
using System.Threading.Tasks;

namespace PottyTrainer.DataModel
{
    public interface IPottyTrainerRepository
    {
        Task<string> SaveEvent(PeePooEvent evt);
        Task<bool> DeleteEvent(int id);
        PeePooEvent GetEvent(int id);
        List<PeePooEvent> GetEvents();
    }
}
