using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;

namespace AcmeSoft.Gui.Controllers
{
    public class PersonsController : Controller
    {
        public PersonsController()
        {
            _client = new HttpClient{BaseAddress = new Uri("http://localhost:54954/") };
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client;

        [Produces(typeof(IEnumerable<PersonViewModel>))]
        public async Task<IActionResult> Index()
        {
            var json = await _client.GetStringAsync("api/Persons");
            var models = string.IsNullOrWhiteSpace(json) ? new List<PersonViewModel>() : JsonConvert.DeserializeObject<IEnumerable<PersonViewModel>>(json);

            var index = new PersonIndexViewModel
            {
                Items = models
            };
            return View(index);
        }

        [HttpGet]
        [Produces(typeof(PersonViewModel))]
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
            var json = await _client.GetStringAsync($"api/Persons/GetByIdNumber/{model.IdNumber}");
            if (!string.IsNullOrWhiteSpace(json))
            {
                ModelState.AddModelError("IdNumber", "Id Number already in use");
            }

            if (ModelState.IsValid)
            {
                var pers = Mapper.Map<Person>(model);
                var resp = await _client.PostAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers, Formatting.Indented), Encoding.UTF8, "application/json"));
                resp.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }

        [HttpGet]
        [Produces(typeof(PersonViewModel))]
        public async Task<IActionResult> Edit(int id)
        {
            var json = await _client.GetStringAsync($"api/Persons/{id}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return NotFound();
            }
            var pers = JsonConvert.DeserializeObject<Person>(json);
            var model = Mapper.Map<PersonViewModel>(pers);
            model.ModelPurpose = ViewModelPurpose.Edit;
            return View("Edit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonViewModel model)
        {
            var json = await _client.GetStringAsync($"api/Persons/GetByIdNumber/{model.IdNumber}");
            if (!string.IsNullOrWhiteSpace(json))
            {
                ModelState.AddModelError("IdNumber", "Id Number already in use");
            }

            if (ModelState.IsValid)
            {
                var pers = Mapper.Map<Person>(model);
                var resp = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers, Formatting.Indented), Encoding.UTF8, "application/json"));
                resp.EnsureSuccessStatusCode();
                return RedirectToAction("Index");
            }

            return View("Edit", model);
        }
    }
}