using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    public class WorkerController : BaseController
    {
        private readonly IWorkerRepository _workerRepository;
        private readonly IWorkerTypeRepository _workerTypeRepository;
        private readonly IStateRepository _stateRepository;

        public WorkerController(
            IWorkerRepository workerRepository,
            IWorkerTypeRepository workerTypeRepository,
            IStateRepository stateRepository)
        {
            _workerRepository = workerRepository;
            _workerTypeRepository = workerTypeRepository;
            _stateRepository = stateRepository;
        }

        // GET: Worker
        [AuthorizeRoles(Role.Administrator)]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Edit()
        {
            var worker = await _workerRepository.GetByUserIdAsync(User.Identity.GetUserId());

            if (worker == null) return HttpNotFound();

            var states = await _stateRepository.GetAllAsync();
            var workerTypes = await _workerTypeRepository.GetAllAsync();

            WorkerEditViewModel viewModel = Mapper.Map<Worker, WorkerEditViewModel>(worker)
                .Map(states)
                .Map(workerTypes);

            return View(viewModel);
        }

        [AuthorizeRoles(Role.Administrator)]
        public async Task<ActionResult> EditByAdmin(int id)
        {
            var worker = await _workerRepository.GetByIdAsync(id);

            if (worker == null) return HttpNotFound();

            var states = await _stateRepository.GetAllAsync();
            var workerTypes = await _workerTypeRepository.GetAllAsync();

            WorkerEditViewModel viewModel = Mapper.Map<Worker, WorkerEditViewModel>(worker)
                .Map(states)
                .Map(workerTypes);

            return View("Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(WorkerEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var states = await _stateRepository.GetAllAsync();
                var workerTypes = await _workerTypeRepository.GetAllAsync();

                viewModel.Map(states).Map(workerTypes);

                return View(viewModel).WithError("Your Profile was unable to be saved");
            }

            Worker worker = Mapper.Map(viewModel, await _workerRepository.GetByIdAsync(viewModel.ID));

            _workerRepository.Update(worker);

            await _workerRepository.SaveAsync();

            return RedirectToAction("Index", "Home").WithSuccess("Profile saved successfully");
        }
    }
}