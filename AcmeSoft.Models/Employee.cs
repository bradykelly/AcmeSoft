using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AcmeSoft.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        public int PersonId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(16)]
        public string EmployeeNum { get; set; }

        [Column(TypeName = "date")]
        public DateTime EmployedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TerminatedDate { get; set; }
    }
}
