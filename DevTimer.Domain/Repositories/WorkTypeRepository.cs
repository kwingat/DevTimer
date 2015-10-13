using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class WorkTypeRepository : Repository<WorkType>, IWorkTypeRepository
    {
        public WorkTypeRepository(DbContextBase context) : base(context)
        {
        }
    }
}