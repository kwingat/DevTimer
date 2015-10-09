using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        public ClientRepository(DbContextBase context) : base(context) { }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        } 

        public async Task<IPagedEnumerable<Client>> GetAllAsync(int pageSize, int pageNumber)
        {
            var query = GetAllQuery();

            int totalRowCount = await query.CountAsync();

            var result = await query.OrderBy(c => c.Name).ForPage(pageNumber, pageSize).ToListAsync();

            return result.AsPagedEnumerable(pageNumber, pageSize, totalRowCount);
        }

        private IQueryable<Client> GetAllQuery()
        {
            return Set
                .OrderBy(e => e.Name);
        }
    }
}
