using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class AspNetUserRepository : Repository<AspNetUser>, IAspNetUserRepository
    {
        public AspNetUserRepository(DbContextBase context) : base(context) { }
        
        public async Task<AspNetUser> GetByIdAsync(string currentUserId)
        {
            return await Set.FindAsync(currentUserId);
        }
    }
}