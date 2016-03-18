using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;

namespace DevTimer.Controllers
{
    [AuthorizeRoles(Role.Administrator)]
    public class ClientController : BaseController
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        //Get: /Client/Edit/0
        public async Task<ActionResult> Edit(int id)
        {
            ClientEditViewModel viewModel;

            if (id > 0) //Existing Client
            {
                Client client = await _clientRepository.GetByIdForEditAsync(id);

                if (client == null)
                {
                    return HttpNotFound();
                }

                viewModel = Mapper.Map<Client, ClientEditViewModel>(client);
            }
            else //New Client
            {
                viewModel = new ClientEditViewModel();
            }

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientEditViewModel viewModel)
        {
            if (!ModelState.IsValid) 
                return View(viewModel).WithError("Client was unable to be saved successfully");

            Client client = Mapper.Map<ClientEditViewModel, Client>(viewModel);

            if (viewModel.ID > 0)
            {
                _clientRepository.Update(client);
                    
            }
            else
            {
                _clientRepository.Add(client);
            }

            await _clientRepository.SaveAsync();

            return RedirectToAction("Index").WithSuccess("Client was saved successfully");
        } 
    }
}