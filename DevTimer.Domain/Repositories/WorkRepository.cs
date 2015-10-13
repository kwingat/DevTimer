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
    }
}