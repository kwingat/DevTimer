using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Models;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    public class TimeController : Controller
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkTypeRepository _workTypeRepository;

        public TimeController(
            IAspNetUserRepository aspNetUserRepository,
            IClientRepository clientRepository,
            IProjectRepository projectRepository,
            IWorkRepository workRepository,
            IWorkTypeRepository workTypeRepository)
        {
            _aspNetUserRepository = aspNetUserRepository;
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
            _workRepository = workRepository;
            _workTypeRepository = workTypeRepository;
        }

        // GET: Work
        public async Task<ActionResult> Index()
        {
            var aspNetUser = await _aspNetUserRepository.GetByIdAsync(User.Identity.GetUserId());
            
            if (aspNetUser == null) 
                return HttpNotFound();

            var works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());

            return View(works.OrderByDescending(w => w.StartTime));
        }
    }
}