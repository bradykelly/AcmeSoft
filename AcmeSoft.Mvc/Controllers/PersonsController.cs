using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.Controllers.Base;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class PersonsController : BaseController
    {
        private readonly IApiClient _apiClient;

        /// <summary>
        /// Instantiates a new <see cref="EmployeesController"/> with injected dependencies where required.
        /// </summary>
        /// <param name="apiClient">An <see cref="IApiClient"/> implementation for injection into the <c>controller</c>.</param>
        /// <param name="config">An <see cref="IConfiguration"/> implementation for injection into the <c>controller</c>.</param>
        public PersonsController(IConfiguration config, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _apiClient.BaseAddress = config["Api:Url"];
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await _apiClient.GetEmployeesAsync();
            var persons = await _apiClient.GetPersonsAsync();

            var emps = employees.Join(persons, emp => emp.PersonId, pers => pers.PersonId, (emp, pers) => new PersonEmployeeViewModel
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

            var model = new PersonEmployeeIndexViewModel()
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = emps
            };
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> GetEmployees(int id)
        {
            var emps = await _apiClient.GetPersonEmployeesAsync(id);
            return Ok(emps);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var pers = new Person();
            var model = Mapper.Map<PersonViewModel>(pers);
            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Persons/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Persons/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Persons/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}