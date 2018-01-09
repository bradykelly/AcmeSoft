using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcmeSoft.Gui.Contracts;
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
        public EmploymentsController(IApiProxy proxy)
        {
            _proxy = proxy;
        }

        private readonly IApiProxy _proxy;

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
            return Ok();
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

        // Child action
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var emp = await _proxy.GetEmploymentByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            var pers = await _proxy.GetPersonById(emp.PersonId);
            var model = Mapper.Map<EmploymentViewModel>(emp);
            model.EmployeeNum = pers.EmployeeNum;
            return View(model);
        }

        // Child action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmploymentViewModel model)
        {
            var emp = Mapper.Map<Employment>(model);
            await _proxy.UpdateEmploymentAsync(emp);
            return Ok();
        }

        // Child action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _proxy.DeleteEmploymentAsync(id);
            return Ok();
        }
    }
}