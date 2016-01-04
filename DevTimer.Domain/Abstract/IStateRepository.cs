using System.Collections.Generic;
using System.Threading.Tasks;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IStateRepository : IRepository
    {
        Task<IEnumerable<State>> GetAllAsync();
    }
}