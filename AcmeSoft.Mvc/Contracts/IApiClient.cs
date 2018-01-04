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
        Task<Person> CreatePersonAsync(Person model);
        Task<List<Employee>> GetEmployeesAsync();
        Task<List<Person>> GetPersonsAsync();
        Task<List<PersonEmployeeViewModel>> GetPersEmpsAsync();
        Task<IEnumerable<PersonViewModel>> GetPersonModelsAsync();
        Task<IEnumerable<EmployeeViewModel>> GetPersonEmployeesAsync(int personId);
        Task<PersonEmployeeViewModel> GetEmployeeAsync(int id);
        Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null);
        Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null);
        Task<PersonEmployeeViewModel> UpdateEmployeeAsync(PersonEmployeeViewModel model);
        Task DeleteEmployeeAsync(PersonEmployeeViewModel model);
        Task<List<string>> GetIdNumbersNamesAsync(string term);
    }
}
