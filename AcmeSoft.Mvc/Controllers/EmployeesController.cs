using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Contracts;
using Microsoft.AspNetCore.Mvc;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IApiClient _apiClient;

        /// <summary>
        /// Instantiates a new <see cref="EmployeesController"/> with injected dependencies where required.
        /// </summary>
        /// <param name="apiClient">An <see cref="IApiClient"/> implementation for injection into the <c>controller</c>.</param>
        /// <param name="config">An <see cref="IConfiguration"/> implementation for injection into the <c>controller</c>.</param>
        public EmployeesController(IConfiguration config, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _apiClient.BaseAddress = config["Api:Url"];
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _apiClient.GetEmployeesAsync();
            var persons = await _apiClient.GetPersonsAsync();

            var emps = employees.Join(persons, emp => emp.PersonId, pers => pers.PersonId, (emp, pers) => new EmployeeViewModel
            {
                LastName = pers.LastName,
                FirstName = pers.FirstName,
                BirthDate = pers.BirthDate.ToString(AppConstants.DefaultDateFormat),
                IdNumber = pers.IdNumber,
                PersonId = emp.PersonId,
                EmployeeId = emp.EmployeeId,
                EmployeeNum = emp.EmployeeNum,
                EmployedDate = emp.EmployedDate.ToString(AppConstants.DefaultDateFormat),
                TerminatedDate = FormatNullableDateTime(emp.TerminatedDate)
            });

            var model = new EmployeeIndexViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = emps
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeViewModel
            {
                ModelPurpose = ViewModelPurpose.Create
            };
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,IdNumber,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.TerminatedDate?.Trim()))
            {
                var employed = DateTime.ParseExact(model.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                var terminated = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                if (terminated <= employed)
                {
                    ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                    ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
                }
            }

            if (await EmployeeNumExistsAsync(model))
            {
                ModelState.AddModelError("EmployeeNum", "Employee number already in use");
            }

            if (ModelState.IsValid)
            {
                await _apiClient.CreateEmployeeAsync(model);
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Details", model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _apiClient.GetEmployeeAsync(id.Value);
            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("EmployeeId,PersonId,LastName,FirstName,BirthDate,IdNumber,EmployeeNum,EmployedDate,TerminatedDate")] EmployeeViewModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.TerminatedDate?.Trim()))
            {
                var employed = DateTime.ParseExact(model.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                var terminated = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                if (terminated <= employed)
                {
                    ModelState.AddModelError("TerminatedDate", "Terminated date must be greater than Employed Date.");
                    ModelState.AddModelError("EmployedDate", "Employed date must be less than or equal to Terminated Date.");
                } 
            }

            if (await EmployeeNumExistsAsync(model))
            {
                ModelState.AddModelError("EmployeeNum", "Employee number already in use");
            }

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(model);

                // Don't know how to do this conditional mapping with AutoMapper. Rather keep it simple.
                if (!string.IsNullOrWhiteSpace(model.TerminatedDate))
                {
                    emp.TerminatedDate = DateTime.ParseExact(model.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                    Mapper.Map(emp, model);
                }
                
                await _apiClient.UpdateEmployeeAsync(model);
                return RedirectToAction(nameof(Index));
            }

            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Details", model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _apiClient.GetEmployeeAsync(id.Value);
            if (model == null)
            {
                return NotFound();
            }

            model.ModelPurpose = ViewModelPurpose.Delete;
            return View("Details", model);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(EmployeeViewModel model)
        {
            await _apiClient.ArchiveEmployeeAsync(model);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> EmployeeNumExistsAsync(EmployeeViewModel model)
        {
            var emp = await _apiClient.GetEmployeeAsync(model.EmployeeId);
            return emp?.Archived != null;
        }

        private string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}
