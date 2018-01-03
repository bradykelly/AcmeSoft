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
        Task<EmployeeAllViewModel> CreateEmployeeAsync(EmployeeAllViewModel model);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Person>> GetPersonsAsync();
        Task<IEnumerable<PersonViewModel>> GetPersonModelsAsync();
        Task<IEnumerable<EmployeeViewModel>> GetPersonEmployeesAsync(int personId);
        Task<EmployeeAllViewModel> GetEmployeeAsync(int id);
        Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null);
        Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null);
        Task<EmployeeAllViewModel> UpdateEmployeeAsync(EmployeeAllViewModel model);
        Task DeleteEmployeeAsync(EmployeeAllViewModel model);
        Task<List<string>> GetIdNumbersNamesAsync(string term);
    }
}
