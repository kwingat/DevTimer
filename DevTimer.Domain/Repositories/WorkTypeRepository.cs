using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(DbContextBase context) : base(context)
        {
        }

        public async Task<IEnumerable<WorkType>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        }

        private IQueryable<WorkType> GetAllQuery()
        {
            return Set.OrderBy(e => e.Name);
        }
    }
}