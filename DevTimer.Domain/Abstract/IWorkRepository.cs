using System.Collections.Generic;
using System.Threading.Tasks;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IWorkRepository : IRepository
    {
        void Add(Work work);
        void Update(Work work);
        Task<IEnumerable<Work>> GetAllByUserAsync(string currentUserId);
        Task<Work> GetByIdAsync(int id);
    }
}