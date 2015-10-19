using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using DevTimer.Domain.Abstract;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    public class WorkController : Controller
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkTypeRepository _workTypeRepository;

        private readonly string _currentUserId; 

        public WorkController(
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

            var _currentUserId = Membership.GetUser(User.Identity.Name);

        }

        // GET: Work
        public async Task<ActionResult> Index()
        {
            var aspNetUser = await _aspNetUserRepository.GetByIdAsync(_currentUserId);
            
            if (aspNetUser == null) return HttpNotFound();

            var clients = await _clientRepository.GetAllAsync();
            var projects = await _projectRepository.GetAllAsync();
            var works = await _workRepository.GetAllByUserAsync(_currentUserId);
            var workTypes = await _workTypeRepository.GetAllAsync();

            

            return View();
        }
    }
}