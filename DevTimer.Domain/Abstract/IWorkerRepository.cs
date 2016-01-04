using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IWorkerRepository : IRepository
    {
        void Add(Worker worker);
        void Update(Worker worker);

        Task<Worker> GetByIdAsync(int id);
        Task<Worker> GetByUserIdAsync(string userId);
        Task<IPagedEnumerable<Worker>> GetAllAsync(int pageSize, int pageNumber);
    }
}