using System.Collections.Generic;
using System.Threading.Tasks;
using AcmeSoft.Shared.Models;

namespace AcmeSoft.Gui.Contracts
{
    public interface IApiProxy
    {
        Task<Person> CreatePerson(Person person);
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person> GetPersonByIdNumber(string idNumber);
        Task<Person> GetPersonById(int personId);
        Task<Person> UpdatePerson(Person person);
        Task DeletePerson(int personId);
        Task<Employment> CreateEmployment(Employment employment);
        Task<IEnumerable<Employment>> GetPersonEmployments(int personId);
        Task<Employment> GetEmploymentByIdAsync(int employmentId);
        Task<Employment> UpdateEmploymentAsync(Employment employment);
    }
}