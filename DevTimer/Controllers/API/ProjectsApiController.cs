using System;
using System.Threading.Tasks;
using System.Web.Http;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Models;

namespace DevTimer.Controllers.API
{
    [RoutePrefix("api/projects")]
    public class ProjectsApiController : ApiController
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectsApiController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetProjects(int pageSize, int pageNumber, string sortOrder = null)
        {
            try
            {
                var projects = await _projectRepository.GetAllAsync(pageSize, pageNumber);
                var dto = projects.ToPageResult<Project, ProjectListViewModel>();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        } 
    }
}