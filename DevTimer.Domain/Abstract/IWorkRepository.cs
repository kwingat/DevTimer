using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IWorkRepository : IRepository
    {
        void Add(Work work);
        void Update(Work work);
    }
}