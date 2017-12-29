using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AcmeSoft.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;
using Newtonsoft.Json;

namespace AcmeSoft.Mvc
{
    public class ApiProxy
    {        
        private readonly HttpClient _client = new HttpClient();

        public async Task<Employee> CreateEmployee(EmployeeViewModel model)
        {
            var cont = new StringContent(JsonConvert.SerializeObject(model));
            var json = _client.PutAsync("api/Employees")
        }

        public async Task<List<Employee>> GetEmployees()
        {
            var json = await _client.GetStringAsync("api/Employees/Get");
            var employees = JsonConvert.DeserializeObject<List<Employee>>(json);
        }

        public async Task<Employee> GetEmployee(int id)
        {            
        }
    }
}
