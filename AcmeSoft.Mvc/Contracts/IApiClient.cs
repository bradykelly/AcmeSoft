﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;

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
        Task ArchiveEmployeeAsync(EmployeeViewModel model);
        Task<List<string>> GetIdNumbersNamesAsync(string term);
    }
}
