using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    [AuthorizeRoles(Role.Administrator)]
    public class WorkerController : BaseController
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IStateRepository _stateRepository;

        public WorkerController(
            IWorkerRepository workerRepository,
            IStateRepository stateRepository)
        {
            _workerRepository = workerRepository;
            _stateRepository = stateRepository;
        }

        // GET: Worker
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit()
        {
            var worker = await _workerRepository.GetByUserIdAsync(User.Identity.GetUserId());

            if (worker == null) return HttpNotFound();

            var states = await _stateRepository.GetAllAsync();

            WorkerEditViewModel viewModel = Mapper.Map<Worker, WorkerEditViewModel>(worker)
                .Map(states);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkerEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var states = await _stateRepository.GetAllAsync();

                viewModel.Map(states);

                return View(viewModel).WithError("Your Profile was unable to be saved");
            }

            Worker worker = Mapper.Map(viewModel, await _workerRepository.GetByIdAsync(viewModel.ID));

            _workerRepository.Update(worker);

            await _workerRepository.SaveAsync();

            return RedirectToAction("Index", "Home").WithSuccess("Profile saved successfully");
        } 
    }
}