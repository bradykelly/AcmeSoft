using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AcmeSoft.Api.Data.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace AcmeSoft.Mvc
{
    public class ApiClient
    {        
        private readonly HttpClient _client = new HttpClient();

        public async Task<EmployeeViewModel> CreateEmployeeAsync(EmployeeViewModel model)
        {
            var pers = Mapper.Map<Person>(model);
            var emp = Mapper.Map<Employee>(model);
            var resp = await _client.PutAsync("api/Persons", new StringContent(JsonConvert.SerializeObject(pers)));
            resp.EnsureSuccessStatusCode();
            var json = resp.Content.ReadAsStringAsync();

            return null;
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
