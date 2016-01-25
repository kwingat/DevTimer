using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using DevTimer.Core;
using DevTimer.Domain.Abstract;
using DevTimer.Domain.Entities;
using DevTimer.Export_to_Excel.Salary;
using DevTimer.Helpers;
using DevTimer.Infrastructure.Alerts;
using DevTimer.Models;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace DevTimer.Controllers
{
    public class TimeController : BaseController
    {
        private readonly IAspNetUserRepository _aspNetUserRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IWorkTypeRepository _workTypeRepository;

        public TimeController(
            IAspNetUserRepository aspNetUserRepository,
            IProjectRepository projectRepository,
            IWorkRepository workRepository,
            IWorkTypeRepository workTypeRepository)
        {
            _aspNetUserRepository = aspNetUserRepository;
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

            var viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works).Map(projects);

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var work = new Work();
            var projects = _projectRepository.GetAll();
            var workTypes = _workTypeRepository.GetAll();

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

                var url = Url.Action("Index", "Time");

                // hide modal
                return Json(new {success = true, url});
            }
            var projects = _projectRepository.GetAll();
            var workTypes = _workTypeRepository.GetAll();
            viewModel.Map(projects).Map(workTypes);

            return RedirectToAction("Index", "Time").WithError("Time unsuccessfully created.");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var work = _workRepository.GetById((int) id);

            if (work == null)
                return HttpNotFound();

            var projects = _projectRepository.GetAll();
            var workTypes = _workTypeRepository.GetAll();

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

                var url = Url.Action("Index", "Time");

                // hide modal
                return Json(new {success = true, url});
            }

            // return invalid state to modal
            return RedirectToAction("Index", "Time").WithError("Time unsuccessfully edited.");
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
            var work = await _workRepository.GetByIdAsync((int) id);

            // if the object is null or the object doesn't belong to use, show an error
            if (work == null || work.UserID != User.Identity.GetUserId())
            {
                works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
                projects = await _projectRepository.GetAllAsync();
                viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works).Map(projects);

                return View("Index", viewModel).WithError("Could not delete.");
            }

            // else delete the record
            _workRepository.Delete(work);
            await _workRepository.SaveAsync();

            works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
            projects = await _projectRepository.GetAllAsync();

            viewModel = Mapper.Map<IEnumerable<Work>, WorkListViewModel>(works)
                .Map(projects);

            // refresh the page to reflect the deletion
            return RedirectToAction("Index", "Time").WithSuccess("Time Successfully deleted.");
            // View("Index", viewModel).WithSuccess("Time successfully deleted.");
        }

        public async Task<ActionResult> Continue(int? id)
        {
            // check if id is null
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            // get object associated with id
            var work = await _workRepository.GetByIdAsync((int) id);

            // check if object is null
            if (work == null)
                return HttpNotFound();
            var newWork = new Work
            {
                ProjectID = work.ProjectID,
                WorkTypeID = work.WorkTypeID,
                Description = work.Description
            };

            var projects = await _projectRepository.GetAllAsync();
            var workTypes = await _workTypeRepository.GetAllAsync();

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
            var work = await _workRepository.GetByIdAsync((int) id);

            // check if object is null
            if (work == null)
                return HttpNotFound();

            var toModel = Mapper.Map<Work, WorkEditViewModel>(work);

            toModel.EndTime = DateTime.Now.ToUniversalTime().ToClientTime();

            var toDomain = Mapper.Map(toModel, await _workRepository.GetByIdAsync(toModel.ID));

            //update and save time object
            _workRepository.Update(toDomain);
            await _workRepository.SaveAsync();

            var works = await _workRepository.GetAllByUserAsync(User.Identity.GetUserId());
            var projects = await _projectRepository.GetAllAsync();

            // Refresh view
            return RedirectToAction("Index", "Time").WithSuccess("Time successfully Closed.");
        }

        public async Task<ActionResult> ExportToExcel(DateTime to, DateTime from) 
        {
            var works = (await _workRepository.GetByUserAndDatesAsync(User.Identity.GetUserId(), to, from)).ToList();
            var timeTrackers = CreateTimeTracker(to, from);
            timeTrackers = CountWorkEachDay(works, timeTrackers);
            var file = Guid.NewGuid() + ".xlsx";
            var package = GenerateReport(timeTrackers, to, from);

            return new DownloadFileActionResult(package, file);
        }

        private static List<TimeTracker> CountWorkEachDay(List<Work> works, List<TimeTracker> timeTrackers)
        {
            foreach (var work in works)
            {
                if (work.StartTime != null && work.Hours != null)
                {
                    var workDate = work.StartTime.Value.Date;
                    var newData = timeTrackers.FirstOrDefault(tt => tt.Date.Equals(workDate));

                    if (newData != null)
                    {
                        switch (work.WorkTypeID)
                        {
                            case 1: //R & D
                                newData.NewDev += work.Hours ?? work.Hours.Value;
                                break;
                            case 2: //Maintenance
                                newData.Maint += work.Hours ?? work.Hours.Value;
                                break;
                            case 3: //Admin
                                newData.Admin += work.Hours ?? work.Hours.Value;
                                break;
                            case 4: //PTO
                                newData.PTO += work.Hours ?? work.Hours.Value;
                                break;
                        }
                    }
                }
            }

            foreach (var timeTracker in timeTrackers)
            {
                timeTracker.DayTotal = timeTracker.NewDev + timeTracker.Maint + timeTracker.Admin + timeTracker.PTO;
            }

            return timeTrackers;
        }

        private static List<TimeTracker> CreateTimeTracker(DateTime startDate, DateTime endDate)
        {
            var timeTrackers = new List<TimeTracker>();

            // Create each day between startdate and enddate
            var date = startDate;
            while (date != endDate.AddDays(1))
            {
                var tt = new TimeTracker
                {
                    Date = date
                };

                timeTrackers.Add(tt);

                date = date.AddDays(1);
            }

            return timeTrackers;
        }

        private ExcelPackage GenerateReport(List<TimeTracker> timeTrackers, DateTime startDate, DateTime endDate)
        {
            var p = new ExcelPackage();

            SetWorkbookProperties(p);
            var ws = CreateSheet(p, "Sheet1");

            //Merge Header
            ws.Cells[1, 1].Value = "Ingenios Health IT Time Tracker";
            var cell = ws.Cells[1, 1, 1, 7];
            cell.Merge = true;
            cell.Style.Font.Bold = true;
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            var fill = cell.Style.Fill;
            fill.PatternType = ExcelFillStyle.Solid;
            fill.BackgroundColor.SetColor(Color.Green);

            ws.Cells[3, 1].Value = "Name:";
            ws.Cells[3, 1].AutoFitColumns();
            ws.Cells[3, 2].Value = User.Identity.Name;
            ws.Cells[3, 2].AutoFitColumns();

            ws.Cells[4, 1].Value = "Period Start:";
            ws.Cells[4, 1].AutoFitColumns();
            ws.Cells[4, 2].Value = startDate.ToShortDateString();
            ws.Cells[4, 2].AutoFitColumns();

            var rowIndex = 6;

            CreateDisplayDataHeader(ws, ref rowIndex, timeTrackers);
            CreateDisplayData(ws, ref rowIndex, timeTrackers);
            CreateDisplayDataFooter(ws, ref rowIndex, timeTrackers);

            return p;
        }

        private static void CreateDisplayDataFooter(ExcelWorksheet ws, ref int rowIndex, List<TimeTracker> timeTrackers)
        {
            var colIndex = 1;
            var type = timeTrackers[1].GetType();
            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var cell = ws.Cells[rowIndex, colIndex];

                var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);

                switch (typeCode)
                {
                    case TypeCode.DateTime:
                        cell.Value = "Totals";
                        break;

                    case TypeCode.Double:
                        cell.Formula = string.Format("SUM({0}:{1})",
                            ws.Cells[(rowIndex - 1) - timeTrackers.Count(), colIndex].Address,
                            ws.Cells[rowIndex - 1, colIndex].Address);
                        break;
                }

                colIndex++;
            }

            rowIndex++;
        }

        private static void CreateDisplayData(ExcelWorksheet ws, ref int rowIndex, List<TimeTracker> timeTrackers)
        {
            foreach (var timeTracker in timeTrackers)
            {
                var colIndex = 1;
                foreach (var propertyInfo in timeTracker.GetType().GetProperties())
                {
                    var cell = ws.Cells[rowIndex, colIndex];
                    var value = propertyInfo.GetValue(timeTracker, null);

                    var typeCode = Type.GetTypeCode(propertyInfo.PropertyType);

                    switch (typeCode)
                    {
                        case TypeCode.DateTime:
                            cell.Style.Numberformat.Format = "ddd M/d/yyyy";
                            cell.Value = value ?? "";
                            cell.AutoFitColumns();
                            break;
                        case TypeCode.Double:
                            var val = (double)(value ?? string.Empty);
                            cell.Value = Math.Round(val, 1);
                            break;
                        case TypeCode.String:
                            cell.Value = value ?? "";
                            break;
                    }

                    //cell.Value = value != null ? value : "";
                    colIndex++;
                }
                rowIndex++;
            }
        }

        private static void CreateDisplayDataHeader(ExcelWorksheet ws, ref int rowIndex, List<TimeTracker> timeTrackers)
        {
            var colIndex = 1;
            var type = timeTrackers[1].GetType();
            var properties = type.GetProperties();

            foreach (var propertyInfo in properties)
            {
                var cell = ws.Cells[rowIndex, colIndex];
                cell.Value = propertyInfo.Name;
                colIndex++;
            }

            rowIndex++;
        }

        private static void SetWorkbookProperties(ExcelPackage p)
        {
            p.Workbook.Properties.Author = "Humpty Dumpty";
            p.Workbook.Properties.Title = "IT Time Tracker";
        }

        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            p.Workbook.Worksheets.Add(sheetName);
            var ws = p.Workbook.Worksheets[1];
            ws.Name = sheetName;
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            return ws;
        }
    }
}