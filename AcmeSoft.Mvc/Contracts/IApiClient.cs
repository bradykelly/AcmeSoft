using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.Contracts
{
    public interface IApiClient
    {
        // NB Align interface and implementation.

        string BaseAddress { get; set; }
        Task<Person> CreatePersonAsync(Person model);
        Task<PersonViewModel> GetByPersonIdAsync(int personId);
        Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null);
        Task<PersonViewModel> UpdatePersonAsync(PersonViewModel model);
        Task DeletePersonAsync(int id);

        Task<EmployeeViewModel> CreateEmployee(int personId);
        Task<IEnumerable<EmployeeViewModel>> GetEmployeesByPersonIdAsync(int personId);

        ////Task<List<PersonEmployeeViewModel>> GetJoinedPersEmpsAsync();
        
        Task<EmployeeViewModel> GetEmployeeAsync(int id);
        
        Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null);
        Task<EmployeeViewModel> UpdateEmployeeAsync(EmployeeViewModel model);
    }
}
