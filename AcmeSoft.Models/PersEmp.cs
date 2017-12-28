using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcmeSoft.Models
{
    [Table("vwPersEmps")]
    public class PersEmp
    {
        [Key]
        public string EmployeeId { get; set; }

        public int PersonId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public string EmployeeNum { get; set; }

        public DateTime EmployedDate { get; set; }

        public DateTime? TerminatedDate { get; set; }
    }
}
