using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data;
using AcmeSoft.Api.Data.Models;
using AcmeSoft.Mvc.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IApiClient _apiClient;
        ////private readonly CompanyContext _dbContext;

        /// <summary>
        /// Instantiates a new <see cref="CompanyContext"/> with injected dependencies where required.
        /// </summary>
        /// <param name="dbContext">A <see cref="DbContext"/> object for injection into the <see cref="CompanyContext"/>.</param>
        // TODO Complete Docs
        public EmployeesController(IConfiguration config, IApiClient apiClient, CompanyContext dbContext)
        {
            _config = config;
            _apiClient = apiClient;
            ////_dbContext = dbContext;

            _apiClient.BaseAddress = _config["Api:Url"];
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

            if (EmployeeNumExists(model))
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

            if (EmployeeNumExists(model))
            {
                ModelState.AddModelError("EmployeeNum", "Employee number already in use");
            }

            if (ModelState.IsValid)
            {
                var emp = Mapper.Map<Employee>(model);

                // Couldn't figure how to do this with AutoMapper.
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiClient.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeNumExists(EmployeeViewModel model)
        {
            // NB Check for create.
            var emp = _apiClient.GetEmployeeAsync(model.EmployeeId);
            return emp != null;
        }

        private string FormatNullableDateTime(DateTime? dateTime)
        {
            return dateTime?.ToString(AppConstants.DefaultDateFormat);
        }
    }
}
