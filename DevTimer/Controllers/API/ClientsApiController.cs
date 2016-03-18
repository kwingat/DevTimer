using System;
using System.Threading.Tasks;
using System.Web.Http;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Models;

namespace DevTimer.Controllers.API
{
    [RoutePrefix("api/clients")]
    public class ClientsApiController : ApiController
    {
        private readonly IClientRepository _clientRepository;

        public ClientsApiController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        // GET: api/clients
        [Route("")]
        public async Task<IHttpActionResult> GetClients(int pageSize, int pageNumber, string sortOrder = null)
        {
            try
            {
                var clients = await _clientRepository.GetAllAsync(pageSize, pageNumber);
                var dto = clients.ToPageResult<Client, ClientListViewModel>();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }

    [RoutePrefix("api/workers")]
    public class WorkersApiController : ApiController
    {
        private readonly IWorkerRepository _workerRepository;

        public WorkersApiController(IWorkerRepository workerRepository)
        {
            _workerRepository = workerRepository;
        }

        [Route("")]
        public async Task<IHttpActionResult> GetWorkers(int pageSize, int pageNumber, string sortOrder = null)
        {
            try
            {
                var workers = await _workerRepository.GetAllAsync(pageSize, pageNumber);
                var dto = workers.ToPageResult<Worker, WorkerListViewModel>();
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        } 
    }
}