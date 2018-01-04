using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Controllers;
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
        public ActionResult Create()
        {
            var pers = new Person();
            var model = Mapper.Map<PersonViewModel>(pers);
            model.BirthDate = null;
            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel model)
        {
            var idPers = await _apiClient.GetByIdNumberAsync(model.IdNumber);
            if (idPers != null && idPers.IdNumber == model.IdNumber)
            {
                ModelState.AddModelError("IdNumber", "This Id Number is already in use.");
            }

            if (ModelState.IsValid)
            {
                var pers = Mapper.Map<Person>(model);
                await _apiClient.CreatePersonAsync(pers);
                return RedirectToAction(nameof(Index));
            }

            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var persEmps = await _apiClient.GetJoinedPersEmpsAsync();
            var model = new PersEmpIndexViewModel()
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = persEmps
            };
            return View(model);
        }


        public async Task<IActionResult> GetByIdNumber(string idNumber)
        {
            var pers = await _apiClient.GetByIdNumberAsync(idNumber);
            return Ok(pers);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var pers = await _apiClient.GetByPersonIdAsync(id);
            var model = Mapper.Map<PersonViewModel>(pers);
            model.BirthDate = model.BirthDate.Substring(0, 10);
            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonViewModel model)
        {
            var idPers = await _apiClient.GetByIdNumberAsync(model.IdNumber);
            if (idPers != null && idPers.IdNumber == model.IdNumber)
            {
                ModelState.AddModelError("IdNumber", "This Id Number is already in use.");
            }

            await _apiClient.UpdatePersonAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _apiClient.GetByPersonIdAsync(id);
            await _apiClient.DeletePersonAsync(id);
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(PersonViewModel model)
        {
            _apiClient.DeletePersonAsync(model.PersonId);
            return RedirectToAction(nameof(Index));
        }
    }
}