using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class StateRepository:Repository<State>, IStateRepository
    {
        public StateRepository(DbContextBase context) : base(context)
        {
        }

        public async Task<IEnumerable<State>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        }

        private IQueryable<State> GetAllQuery()
        {
            return Set
                .OrderBy(e => e.StateName);
        }
    }
}