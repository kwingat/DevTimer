using System.Threading.Tasks;

namespace DevTimer.Domain.Abstract
{
    public interface IRepository
    {
        void Save();
        Task SaveAsync();
    }
}
