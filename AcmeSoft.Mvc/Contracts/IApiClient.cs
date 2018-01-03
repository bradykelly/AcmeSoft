using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.Contracts
{
    public interface IApiClient
    {
        string BaseAddress { get; set; }
        Task<EmployeeViewModel> CreateEmployeeAsync(EmployeeViewModel model);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Person>> GetPersonsAsync();
        Task<List<PersonViewModel>> GetPersonModelsAsync();
        Task<EmployeeViewModel> GetEmployeeAsync(int id);
        Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null);
        Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null);
        Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel model);
        Task DeleteEmployeeAsync(EmployeeViewModel model);
        Task<List<string>> GetIdNumbersNamesAsync(string term);
    }
}
