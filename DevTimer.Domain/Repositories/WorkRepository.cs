using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class WorkRepository : Repository<Work>, IWorkRepository
    {
        public WorkRepository(DbContextBase context) : base(context)
        {
        }

        public void Add(Work work)
        {
            AddEntity(work);
        }

        public void Update(Work work)
        {
            AddEntity(work);
        }

        public async Task<IEnumerable<Work>> GetAllByUserAsync(string currentUserId)
        {
            return await GetAllByUserQuery(currentUserId).ToListAsync();
        }

        private IQueryable<Work> GetAllByUserQuery(string currentUserId)
        {
            return Set.OrderByDescending(e => e.StartTime);
        }
    }
}