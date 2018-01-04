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
        Task<PersonViewModel> GetByPersonIdAsync(int personId);
        Task<Person> GetByIdNumberAsync(string idNumber, int? excludeId = null);
        Task<PersonViewModel> UpdatePersonAsync(PersonViewModel model);
        Task DeletePersonAsync(int id);

        Task<List<PersonEmployeeViewModel>> GetJoinedPersEmpsAsync();
        Task<List<Employee>> GetEmployeesAsync();

        Task<IEnumerable<EmployeeViewModel>> GetPersonEmployeesAsync(int personId);
        Task<PersonEmployeeViewModel> GetEmployeeAsync(int id);
        
        Task<Employee> GetByEmpNumAsync(string empNumber, int? excludeId = null);
        Task<PersonEmployeeViewModel> UpdateEmployeeAsync(PersonEmployeeViewModel model);
    }
}
