using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AcmeSoft.Api.Data.Models;
using AcmeSoft.Mvc.Contracts;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace AcmeSoft.Mvc
{
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

        public async Task<List<Employee>> GetEmployees()
        {
            var json = await _client.GetStringAsync("api/Employees/Get");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
            return employees;
        }

        public async Task<EmployeeViewModel> GetEmployee(int id)
        {
            var json = await _client.GetStringAsync($"api/Employees/{id}");
            var emp = JsonConvert.DeserializeObject<Employee>(json);
            json = await _client.GetStringAsync($"api/Person/{emp.PersonId}");
            var pers = JsonConvert.DeserializeObject<Person>(json);

            var model = Mapper.Map<EmployeeViewModel>(emp);
            Mapper.Map(pers, model);
            return model;
        }
    }
}
