using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class WorkerTypeRepository : Repository<WorkerType>, IWorkerTypeRepository
    {
        public WorkerTypeRepository(DbContextBase context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkerType>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        }

        private IQueryable<WorkerType> GetAllQuery()
        {
            return Set.OrderBy(e => e.Type);
        }
    }
}