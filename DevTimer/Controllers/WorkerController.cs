using System.Web.Mvc;
using DevTimer.Domain.Abstract;

namespace DevTimer.Controllers
{
    [Authorize]
    public class WorkerController : BaseController
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkerController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        // GET: Worker
        public ActionResult Index()
        {
            return View();
        }
    }
}