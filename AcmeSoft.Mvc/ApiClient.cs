using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AcmeSoft.Api.Data.Models;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        public async Task<EmployeeViewModel> CreateEmployeeAsync(EmployeeViewModel model)
        {
            var person = Mapper.Map<Person>(model);

            // Check if the Person already exists and if not, create a Person.
            string json;
            Person pers;
            using (var tx = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                json = await _client.GetStringAsync($"api/Persons/GetByIdNumber/{person.IdNumber}");
                if (string.IsNullOrWhiteSpace(json))
                {
                    var respP = await _client.PostAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(person, Formatting.Indented), Encoding.UTF8, "application/json"));
                    respP.EnsureSuccessStatusCode();
                    json = await respP.Content.ReadAsStringAsync();
                }

                pers = JsonConvert.DeserializeObject<Person>(json);

                var employee = Mapper.Map<Employee>(model);
                employee.PersonId = pers.PersonId;

                // Always create a new Employee.
                var respE = await _client.PostAsync("api/Employees", new StringContent(JsonConvert.SerializeObject(employee, Formatting.Indented), Encoding.UTF8, "application/json"));
                respE.EnsureSuccessStatusCode();
                tx.Complete();
                json = await respE.Content.ReadAsStringAsync();
            }

            var emp = JsonConvert.DeserializeObject<Employee>(json);

            var retModel = Mapper.Map<EmployeeViewModel>(emp);
            Mapper.Map(pers, retModel);

            return retModel;
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            var json = await _client.GetStringAsync("api/Employees");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            return employees;
        }

        public async Task<EmployeeViewModel> GetEmployeeAsync(int id)
        {
            var json = await _client.GetStringAsync($"api/Employees/{id}");
            if (json == null)
            {
                return null;
            }
            var emp = JsonConvert.DeserializeObject<Employee>(json);

            json = await _client.GetStringAsync($"api/Persons/{emp.PersonId}");
            if (json == null)
            {
                return null;
            }

            var pers = JsonConvert.DeserializeObject<Person>(json);
            var model = Mapper.Map<EmployeeViewModel>(emp);
            Mapper.Map(pers, model);
            return model;
        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            var json = await _client.GetStringAsync("api/Persons");
            var persons = JsonConvert.DeserializeObject<List<Person>>(json);
            return persons;
        }

        public async Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel model)
        {
            var pers = Mapper.Map<Person>(model);
            var respP = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers), Encoding.UTF8, "application/json"));
            respP.EnsureSuccessStatusCode();
            var json = await respP.Content.ReadAsStringAsync();
            pers = JsonConvert.DeserializeObject<Person>(json);

            var emp = Mapper.Map<Employee>(model);
            var respE = await _client.PutAsync("api/Employees", new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json"));
            respE.EnsureSuccessStatusCode();
            json = await respE.Content.ReadAsStringAsync();
            emp = JsonConvert.DeserializeObject<Employee>(json);

            // Populate the return model with the updated Person and Employee records.
            var retModel = Mapper.Map<EmployeeViewModel>(pers);
            Mapper.Map(emp, retModel);
            return retModel;
        }

        public async Task DeleteEmployeeAsync(EmployeeViewModel model)
        {
            // Always delete the Employee.
            var respE = await _client.DeleteAsync($"api/Employees/{model.EmployeeId}");
            respE.EnsureSuccessStatusCode();

            // Delete the Person if it has no Employees.
            var jsonE = await _client.GetStringAsync($"api/Employees/GetByPersonId/{model.PersonId}");
            var emps = JsonConvert.DeserializeObject<List<Employee>>(jsonE);
            if (!emps.Any())
            {
                var respP = await _client.DeleteAsync($"api/Persons/{model.PersonId}");
                respP.EnsureSuccessStatusCode();
            }
        }
    }
}
