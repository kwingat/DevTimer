using System.Collections.Generic;
using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Abstract
{
    public interface IProjectRepository : IRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        IEnumerable<Project> GetAll();
        Task<IPagedEnumerable<Project>> GetAllAsync(int pageSize, int pageNumber);

        Task<Project> GetByIdForEditAsync(int projectId);

        void Add(Project project);
        void Update(Project project);
        
    }
}