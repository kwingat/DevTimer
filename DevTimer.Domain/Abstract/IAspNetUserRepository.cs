using System.Threading.Tasks;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IAspNetUserRepository : IRepository
    {
        Task<AspNetUser> GetByIdAsync(string currentUserId);
    }
}