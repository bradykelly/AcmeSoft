using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Api.Data.Models;
using AcmeSoft.Mvc.ViewModels;

namespace AcmeSoft.Mvc.Contracts
{
    public interface IApiClient
    {
        string BaseAddress { get; set; }
        Task<EmployeeViewModel> CreateEmployeeAsync(EmployeeViewModel model);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Person>> GetPersonsAsync();
        Task<EmployeeViewModel> GetEmployeeAsync(int id);
        Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel model);
        Task DeleteEmployeeAsync(EmployeeViewModel model);
    }
}
