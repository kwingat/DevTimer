using System.Collections.Generic;
using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IClientRepository : IRepository
    {
        Task<IEnumerable<Client>> GetAllAsync();
        Task<IPagedEnumerable<Client>> GetAllAsync(int pageSize, int pageNumber);

        Task<Client> GetByIdForEditAsync(int clientId);

        void Add(Client client);
        void Update(Client client);
    }
}
