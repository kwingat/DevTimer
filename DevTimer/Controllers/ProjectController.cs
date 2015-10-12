using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;

namespace DevTimer.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // Get: Project
        public ActionResult Index()
        {
            return View();
        }

        //Get: /Project/Edit/0
        public async Task<ActionResult> Edit(int id)
        {
            ProjectEditViewModel viewModel;

            if (id > 0) // Existing Project
            {
                Project project = await _projectRepository.GetByIdForEditAsync(id);

                if (project == null)
                {
                    return HttpNotFound();
                }

                viewModel = Mapper.Map<Project, ProjectEditViewModel>(project);
            }
            else
            {
                viewModel = new ProjectEditViewModel();
            }

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProjectEditViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel).WithError("Project was unable to be saved successfully");

            Project project = Mapper.Map<ProjectEditViewModel, Project>(viewModel);

            if (viewModel.ID > 0)
            {
                _projectRepository.Update(project);
            }
            else
            {
                _projectRepository.Add(project);
            }

            await _projectRepository.SaveAsync();

            return RedirectToAction("Index").WithSuccess("Project was saved successfully");
        } 
    }
}