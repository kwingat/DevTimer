using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;

namespace DevTimer.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: Client
        public async Task<ActionResult> Index()
        {
            IEnumerable<Client> x = await _clientRepository.GetAllAsync();

            ViewBag.X = x;

            return View();
        }
    }
}