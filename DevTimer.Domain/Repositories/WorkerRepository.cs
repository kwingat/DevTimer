using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class WorkerRepository : Repository<Worker>, IWorkerRepository
    {
        public WorkerRepository(DbContextBase context) : base(context)
        {
        }

        public void Add(Worker worker)
        {
            AddEntity(worker);
        }

        public void Update(Worker worker)
        {
            UpdateEntity(worker);
        }

        public async Task<Worker> GetByIdAsync(int id)
        {
            return await Set.FindAsync(id);
        }

        public async Task<IPagedEnumerable<Worker>> GetAllAsync(int pageSize, int pageNumber)
        {
            IQueryable<Worker> query = GetAllQuery();

            int totalRowCount = await query.CountAsync();

            List<Worker> result = await query.OrderBy(w => w.Name)
                .ForPage(pageNumber, pageSize).ToListAsync();

            return result.AsPagedEnumerable(pageNumber, pageSize, totalRowCount);
        }

        private IQueryable<Worker> GetAllQuery()
        {
            return Set
                .Include(e => e.State)
                .Include(e => e.AspNetUser)
                .OrderBy(e => e.Name);
        } 
    }
}