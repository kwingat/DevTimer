using System.Collections.Generic;
using System.Threading.Tasks;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IWorkTypeRepository : IRepository
    {
        Task<IEnumerable<WorkType>> GetAllAsync();
    }
}