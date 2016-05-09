using System.Collections.Generic;
using System.Threading.Tasks;

namespace PottyTrainer.Contracts
{
    public interface IPottyTrainerRepository
    {
        Task<int> SaveEvent(PeePooEvent evt);
        Task<bool> DeleteEvent(int id);
        PeePooEvent GetEvent(int id);
        List<PeePooEvent> GetEvents();
    }
}
