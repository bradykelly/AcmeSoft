using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SQLitePCL;

namespace AcmeSoft.Mvc
{
    /// <summary>
    /// Handles all HTTP API request/response transactions to the API.
    /// </summary>
    public class ApiClient : IApiClient
    {
        public ApiClient()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private readonly HttpClient _client = new HttpClient();

        public string BaseAddress
        {
            get => _client.BaseAddress.ToString();
            set
            {
                if (_client.BaseAddress != null)
                {
                    return;
                }
                _client.BaseAddress = new Uri(value);
            }
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            var resp = await _client.PostAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Person>(json);
        }
    
        public async Task<PersonViewModel> GetByPersonIdAsync(int personId)
        {
            var json = await _client.GetStringAsync($"api/Persons/{personId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var pers = JsonConvert.DeserializeObject<PersonViewModel>(json);
            return pers;
        }

        [HttpGet]
        public async Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null)
        {
            var json = await _client.GetStringAsync($"api/Persons/GetByIdNumber/{idNumber}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var pers = JsonConvert.DeserializeObject<Person>(json);
            if (excludeId.HasValue && pers.PersonId == excludeId)
            {
                return null;
            }
            return pers;
        }

        public async Task<PersonViewModel> UpdatePersonAsync(PersonViewModel model)
        {
            var pers = Mapper.Map<Person>(model);
            var resp = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            pers = JsonConvert.DeserializeObject<Person>(json);
            return Mapper.Map<PersonViewModel>(pers);
        }

        public async Task DeletePersonAsync(int id)
        {
            var resp = await _client.DeleteAsync($"api/Persons/{id}");
            resp.EnsureSuccessStatusCode();
        }

        public async Task<EmployeeViewModel> CreateEmployeeAsync(int personId)
        {
            var emp = new Employee
            {
                PersonId = personId
            };
           
            var resp = await _client.PostAsync("api/Employees", new StringContent(JsonConvert.SerializeObject(emp, Formatting.Indented), Encoding.UTF8, "application/json"));
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            emp = JsonConvert.DeserializeObject<Employee>(json);
            var model = Mapper.Map<EmployeeViewModel>(emp);
            return model;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployeesByPersonIdAsync(int personId)
        {
            var json = await _client.GetStringAsync("api/Persons/GetByPersonId");
            var persons = JsonConvert.DeserializeObject<List<Employee>>(json);
            var models = Mapper.Map<IEnumerable<EmployeeViewModel>>(persons);
            return models;
        }








        ////public async Task<List<PersonEmployeeViewModel>> GetJoinedPersEmpsAsync()
        ////{
        ////    var json = await _client.GetStringAsync("api/Persons/PersonEmployees");
        ////    if (string.IsNullOrWhiteSpace(json))
        ////    {
        ////        return new List<PersonEmployeeViewModel>();
        ////    }
        ////    var persons = JsonConvert.DeserializeObject<List<PersonEmployee>>(json);
        ////    return Mapper.Map<List<PersonEmployeeViewModel>>(persons);
        ////}

        public async Task<IEnumerable<PersonViewModel>> GetPersonsAsync()
        {
            var json = await _client.GetStringAsync("api/Persons");
            var persons = JsonConvert.DeserializeObject<List<PersonViewModel>>(json);
            var models = Mapper.Map<IEnumerable<PersonViewModel>>(persons);
            return models;
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(int id)
        {
            // Get the employee by id.
            var json = await _client.GetStringAsync($"api/Employees/{id}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var emp = JsonConvert.DeserializeObject<Employee>(json);

            // Get the person linked to the Employee.
            json = await _client.GetStringAsync($"api/Persons/{emp.PersonId}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var pers = JsonConvert.DeserializeObject<Person>(json);

            // Build up and return a viewmodel.
            var model = Mapper.Map<EmployeeViewModel>(emp);
            Mapper.Map(pers, model);
            return model;
        }


        public async Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null)
        {
            var json = await _client.GetStringAsync($"api/Employees/GetByEmpNumber/{empNumber}");
            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }
            var emp = JsonConvert.DeserializeObject<Employee>(json);
            if (excludeId.HasValue && emp.EmployeeId == excludeId)
            {
                return null;
            }
            return emp;
        }

        public async Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel model)
        {
            // Update the Person and then read back the new Person part of the model.
            var pers = Mapper.Map<Person>(model);
            var respP = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers), Encoding.UTF8, "application/json"));
            respP.EnsureSuccessStatusCode();
            var json = await respP.Content.ReadAsStringAsync();
            pers = JsonConvert.DeserializeObject<Person>(json);

            // Update the Employee and then read back the new Employee part of the model.
            var emp = Mapper.Map<Employee>(model);
            var respE = await _client.PutAsync("api/Employees", new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json"));
            respE.EnsureSuccessStatusCode();
            json = await respE.Content.ReadAsStringAsync();
            emp = JsonConvert.DeserializeObject<Employee>(json);

            // Populate the return model with the updated Person and Employee records.
            var empModel = Mapper.Map<EmployeeViewModel>(pers);
            Mapper.Map(emp, empModel);
            return empModel;
        }

        public async Task DeleteEmployeeAsync(EmployeeViewModel model)
        {
            using (var tx = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                // Delete the Employee.
                var respE = await _client.DeleteAsync($"api/Employees/{model.EmployeeId}");
                respE.EnsureSuccessStatusCode();

                // Delete the Person.
                var respP = await _client.DeleteAsync($"api/Persons/{model.PersonId}");
                respP.EnsureSuccessStatusCode(); 

                tx.Complete();
            }
        }
    }
}
