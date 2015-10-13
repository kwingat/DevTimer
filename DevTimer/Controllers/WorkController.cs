using System.Web.Mvc;
using DevTimer.Domain.Abstract;

namespace DevTimer.Controllers
{
    public class WorkController : Controller
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkTypeRepository _workTypeRepository;

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

        }

        // GET: Work
        public ActionResult Index()
        {

            return View();
        }
    }
}