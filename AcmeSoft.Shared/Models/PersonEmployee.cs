using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeSoft.Shared.Models
{
    [Table("vwPersEmp")]
    public class PersonEmployee
    {
        // This class maps to a view, i.e. read only, so no validation attributes are required.

        [Key]
        public int PersonId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string IdNumber { get; set; }
        
        public int? EmployeeId { get; set; }

        public string EmployeeNum { get; set; }

        public DateTime? EmployedDate { get; set; }

        public DateTime? TerminatedDate { get; set; }
    }
}