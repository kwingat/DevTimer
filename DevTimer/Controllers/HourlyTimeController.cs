using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Helpers;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;
using Microsoft.AspNet.Identity;

namespace DevTimer.Controllers
{
    public class HourlyTimeController : Controller
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkTypeRepository _workTypeRepository;

        public HourlyTimeController(
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

        // GET: HourlyTime
        public async Task<ActionResult> Index()
        {
            var aspNetUser = await _aspNetUserRepository.GetByIdAsync(User.Identity.GetUserId());

            if (aspNetUser == null) return HttpNotFound();

            var works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
            var projects = await _projectRepository.GetAllAsync();

            var viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works).Map(projects);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var work = new Work();
            //TODO: make this cleaner
            var projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
            var workTypes = _workTypeRepository.GetAll().Where(p => p.Name.Equals("Administration") || p.Name.Equals("PTO"));

            var viewModel = Mapper.Map<Work, WorkEditViewModel>(work).Map(projects).Map(workTypes);

            return PartialView("_Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var work = Mapper.Map<WorkEditViewModel, Work>(viewModel);
                work.UserID = User.Identity.GetUserId();
                _workRepository.Add(work);
                _workRepository.Save();

                var url = Url.Action("Index", "HourlyTime");

                // hide modal
                return Json(new { success = true, url });
            }
            var projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
            var workTypes = _workTypeRepository.GetAll().Where(p => p.Name.Equals("Administration") || p.Name.Equals("PTO"));
            viewModel.Map(projects).Map(workTypes);

            return RedirectToAction("Index", "HourlyTime").WithError("Time unsuccessfully created.");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var work = _workRepository.GetById((int)id);

            if (work == null)
                return HttpNotFound();

            var projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
            var workTypes = _workTypeRepository.GetAll().Where(p => p.Name.Equals("Administration") || p.Name.Equals("PTO"));

            var viewModel = Mapper.Map<Work, WorkEditViewModel>(work)
                .Map(projects)
                .Map(workTypes);

            return PartialView("_Edit", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkEditViewModel viewModel)
        {
            // check if page has errors
            if (ModelState.IsValid)
            {
                // map viewmodel to entity
                var work = Mapper.Map(viewModel, _workRepository.GetById(viewModel.ID));

                // save
                _workRepository.Update(work);
                _workRepository.Save();

                var url = Url.Action("Index", "HourlyTime");

                // hide modal
                return Json(new { success = true, url });
            }

            // return invalid state to modal
            return RedirectToAction("Index", "HourlyTime").WithError("Time unsuccessfully edited.");
        }

        public async Task<ActionResult> Delete(int? id)
        {
            // check id for null
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            IEnumerable<Work> works;
            IEnumerable<Project> projects;
            WorkListViewModel viewModel;

            // get object associated with id
            var work = await _workRepository.GetByIdAsync((int)id);

            // if the object is null or the object doesn't belong to use, show an error
            if (work == null || work.UserID != User.Identity.GetUserId())
            {
                works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
                projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
                viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works).Map(projects);

                return View("Index", viewModel).WithError("Could not delete.");
            }

            // else delete the record
            _workRepository.Delete(work);
            await _workRepository.SaveAsync();

            works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
            projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));

            //viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works).Map(projects);

            // refresh the page to reflect the deletion
            return RedirectToAction("Index", "HourlyTime").WithSuccess("Time Successfully deleted.");
            // View("Index", viewModel).WithSuccess("Time successfully deleted.");
        }

        public async Task<ActionResult> Continue(int? id)
        {
            // check if id is null
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // get object associated with id
            var work = await _workRepository.GetByIdAsync((int)id);

            // check if object is null
            if (work == null)
                return HttpNotFound();
            var newWork = new Work
            {
                ProjectID = work.ProjectID,
                WorkTypeID = work.WorkTypeID,
                Description = work.Description
            };

            var projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
            var workTypes = _workTypeRepository.GetAll().Where(p => p.Name.Equals("Administration") || p.Name.Equals("PTO"));

            var viewModel = Mapper.Map<Work, WorkEditViewModel>(newWork)
                .Map(projects)
                .Map(workTypes);

            // Display modal
            return PartialView("_Create", viewModel);
        }

        public async Task<ActionResult> Close(int? id)
        {
            // check if id is null
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // get object associated with id
            var work = await _workRepository.GetByIdAsync((int)id);

            // check if object is null
            if (work == null)
                return HttpNotFound();

            var toModel = Mapper.Map<Work, WorkEditViewModel>(work);

            toModel.EndTime = DateTime.Now.ToUniversalTime().ToClientTime();

            var toDomain = Mapper.Map(toModel, await _workRepository.GetByIdAsync(toModel.ID));

            //update and save time object
            _workRepository.Update(toDomain);
            await _workRepository.SaveAsync();

            var projects = _projectRepository.GetAll().Where(p => p.Name.Equals("Call Center"));
            var workTypes = _workTypeRepository.GetAll().Where(p => p.Name.Equals("Administration") || p.Name.Equals("PTO"));

            // Refresh view
            return RedirectToAction("Index", "HourlyTime").WithSuccess("Time successfully Closed.");
        }
    }
}