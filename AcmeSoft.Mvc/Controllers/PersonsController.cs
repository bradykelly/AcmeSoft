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
            var persEmps = await _apiClient.GetPersEmpsAsync();

            var model = new PersEmpIndexViewModel()
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = persEmps
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
            model.BirthDate = null;
            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonViewModel model)
        {
            // NB Check for dup Id.

            if (ModelState.IsValid)
            {
                var pers = Mapper.Map<Person>(model);
                _apiClient.CreatePersonAsync(pers);
                return RedirectToAction(nameof(Index));
            }

            return View("Details", model);
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