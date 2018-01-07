using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcmeSoft.Gui.Controllers.Base;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.Services;
using AcmeSoft.Gui.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AcmeSoft.Gui.Controllers
{
    public class EmploymentsController : BaseController
    {
        // NB Rather inject. The proxy interface could fit many APIs.
        private ApiProxy _proxy = new ApiProxy();

        // Child action
        [HttpGet]
        public async Task<IActionResult> Create(int personId)
        {
            var emp = new Employment();
            var model = Mapper.Map<EmploymentViewModel>(emp);
            var pers = await _proxy.GetPersonById(personId);           
            model.PersonId = pers.PersonId;
            model.EmployeeNum = pers.EmployeeNum;
            model.EmployedDate = null;
            model.ModelPurpose = ViewModelPurpose.Create;
            return View("_EmploymentForm", model);
        }

        // Child action
        [HttpPost]
        public async Task<IActionResult> Create(EmploymentViewModel model)
        {
            var employment = Mapper.Map<Employment>(model);
            await _proxy.CreateEmployment(employment);
            return RedirectToAction("Edit", "Persons", new {id = model.PersonId});
        }

        // Child action
        [HttpGet]
        public async Task<IActionResult> Index(int personId)
        {
            var emps = await _proxy.GetPersonEmployments(personId);
            var vms = Mapper.Map<IEnumerable<EmploymentViewModel>>(emps);
            var model = new EmploymentIndexViewModel
            {
                Items = vms,
                PersonId = personId
            };
            return PartialView("_EmploymentsTable", model);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
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

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
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