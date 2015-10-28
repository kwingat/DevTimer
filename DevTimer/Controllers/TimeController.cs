using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;
using AutoMapper;
using DevTimer.Core;
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
            var projects = await _projectRepository.GetAllAsync();

            WorkListViewModel viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works)
                .Map(projects);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            Work work = new Work();
            IEnumerable<Project> projects = _projectRepository.GetAll();
            IEnumerable<WorkType> workTypes = _workTypeRepository.GetAll();

            WorkEditViewModel viewModel = Mapper.Map<Work, WorkEditViewModel>(work)
                .Map(projects)
                .Map(workTypes);

            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Work work = Mapper.Map<WorkEditViewModel, Work>(viewModel);
                work.UserID = User.Identity.GetUserId();
                _workRepository.Add(work);
                _workRepository.Save();

                return Json(new {success = true});
            }
            IEnumerable<Project> projects = _projectRepository.GetAll();
            IEnumerable<WorkType> workTypes = _workTypeRepository.GetAll();
            viewModel.Map(projects).Map(workTypes);

            return PartialView("_Create", viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Work work = _workRepository.GetById((int) id);

            if (work == null)
                return HttpNotFound();

            IEnumerable<Project> projects = _projectRepository.GetAll();
            IEnumerable<WorkType> workTypes = _workTypeRepository.GetAll();

            WorkEditViewModel viewModel = Mapper.Map<Work, WorkEditViewModel>(work)
                .Map(projects)
                .Map(workTypes);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Work work = Mapper.Map(viewModel, _workRepository.GetById(viewModel.ID));
                //work.UserID = User.Identity.GetUserId();
                _workRepository.Update(work);
                _workRepository.Save();

                string url = Url.Action("Index", "Time");

                return Json(new { success = true, url });
            }

            return PartialView("_Edit", viewModel);
        }
    }
}