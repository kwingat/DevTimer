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

        public async Task<Work> GetByIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public Work GetById(int id)
        {
            return Set.Find(id);
        }

        private IQueryable<Work> GetAllByUserQuery(string currentUserId)
        {
            var query = Set
                .Include(e => e.AspNetUser)
                .Include(e => e.Project)
                .Include(e => e.Project.Client)
                .Include(e => e.WorkType)
                .Where(e => e.UserID == currentUserId)
                .OrderByDescending(e => e.StartTime);

            return query;
        }
    }
}