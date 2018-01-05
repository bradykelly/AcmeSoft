using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AcmeSoft.Mvc.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IApiClient _apiClient;

        public EmployeesController(IConfiguration config, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _apiClient.BaseAddress = config["Api:Url"];
        }

        [HttpGet]
        public ActionResult Create(int personId)
        {
            var emp = new Employee
            {
                PersonId = personId
            };
            var model = Mapper.Map<EmployeeViewModel>(emp);
            model.ModelPurpose = ViewModelPurpose.Create;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Create(EmployeeViewModel model)
        {
            var emp = Mapper.Map<Employee>(model);
            await _apiClient.CreateEmployeeAsync(emp);
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<EmployeeViewModel>))]
        public async Task<IActionResult> GetByPersonId(int personId)
        {
            var emps = await _apiClient.GetEmployeesByPersonIdAsync(personId);
            var models = Mapper.Map<IEnumerable<EmployeeViewModel>>(emps);
            return Ok(models);
        }

        [HttpGet]            
        public ActionResult Details(int id)
        {
            return View();
        }




        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _apiClient.GetEmployeeAsync(id);
            var model = Mapper.Map<EmployeeViewModel>(emp);
            return View("Details", model);
        }

        ////// POST: Employmee/Edit/5
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Edit(EmployeeViewModel model)
        ////{
        ////    // TODO: Add update logic here

        ////    return RedirectToAction(nameof(Index));
        ////}

        // GET: Employmee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        ////// POST: Employmee/Delete/5
        ////[HttpPost]
        ////[ValidateAntiForgeryToken]
        ////public ActionResult Delete(int id, IFormCollection collection)
        ////{
        ////    try
        ////    {
        ////        // TODO: Add delete logic here

        ////        return RedirectToAction(nameof(Index));
        ////    }
        ////    catch
        ////    {
        ////        return View();
        ////    }
        ////}
    }
}