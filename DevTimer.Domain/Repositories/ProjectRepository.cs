using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Domain.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(DbContextBase context) : base(context) { }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await GetAllQuery().ToListAsync();
        }

        public async Task<IPagedEnumerable<Project>> GetAllAsync(int pageSize, int pageNumber)
        {
            var query = GetAllQuery();

            int totalRowCount = await query.CountAsync();

            var results = await query.OrderBy(c => c.Name).ForPage(pageNumber, pageSize).ToListAsync();

            return results.AsPagedEnumerable(pageNumber, pageSize, totalRowCount);
        }

        public async Task<Project> GetByIdForEditAsync(int projectId)
        {
            return await Set.FindAsync(projectId);
        }

        public void Add(Project project)
        {
            AddEntity(project);
        }

        public void Update(Project project)
        {
            UpdateEntity(project);
        }

        private IQueryable<Project> GetAllQuery()
        {
            return Set
                .OrderBy(e => e.Name);
        }
    }
}