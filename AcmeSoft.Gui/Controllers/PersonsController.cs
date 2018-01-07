using System;
using System.Collections;
using System.Collections.Generic;
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace AcmeSoft.Gui.Controllers
{
    public class PersonsController : BaseController
    {
        private readonly ApiProxy _proxy = new ApiProxy();

        [HttpGet]
        [Produces(typeof(PersonViewModel))]
        public ActionResult Create()
        {
            var pers = new Person();
            var model = Mapper.Map<PersonViewModel>(pers);
            // NB Get right in mapping.
            model.BirthDate = null;
            model.ModelPurpose = ViewModelPurpose.Create;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonViewModel model)
        {
            var pers = await _proxy.GetPersonByIdNumber(model.IdNumber);
            if (pers != null)
            {
                ModelState.AddModelError("IdNumber", "Id Number already in use");
            }

            if (ModelState.IsValid)
            {
                pers = Mapper.Map<Person>(model);
                await _proxy.CreatePerson(pers);
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        [HttpGet]
        [Produces(typeof(IEnumerable<PersonViewModel>))]
        public async Task<IActionResult> Index()
        {
            var persons = await _proxy.GetAllPersons();
            var models = Mapper.Map<IEnumerable<PersonViewModel>>(persons);
            var index = new PersonIndexViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = models
            };
            return View(index);
        }

        // NB Make employments a View Component.
        [HttpGet]
        [Produces(typeof(IEnumerable<EmploymentViewModel>))]
        public async Task<IActionResult> GetEmployments(int personId)
        {
            var emps = await _proxy.GetPersonEmployments(personId);
            var vms = Mapper.Map<IEnumerable<EmploymentViewModel>>(emps);
            var model = new EmploymentIndexViewModel
            {
                ModelPurpose = ViewModelPurpose.Index,
                Items = vms
            };
            return View("_EmploymentsTable", model);
        }

        [HttpGet]
        [Produces(typeof(PersonViewModel))]
        public async Task<IActionResult> Edit(int personId)
        {
            var pers = await _proxy.GetPersonById(personId);
            var model = Mapper.Map<PersonViewModel>(pers);
            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonViewModel model)
        {
            var pers = await _proxy.GetPersonByIdNumber(model.IdNumber);
            if (pers != null)
            {
                ModelState.AddModelError("IdNumber", "Id Number already in use");
            }

            if (ModelState.IsValid)
            {
                pers = Mapper.Map<Person>(model);
                await _proxy.UpdatePerson(pers);
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var pers = await _proxy.GetPersonById(id);
            var model = Mapper.Map<PersonViewModel>(pers);
            model.ModelPurpose = ViewModelPurpose.Delete;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(PersonViewModel model)
        {
            await _proxy.DeletePerson(model.PersonId);
            return RedirectToAction("Index");
        }
    }
}