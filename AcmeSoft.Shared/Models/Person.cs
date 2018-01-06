using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeSoft.Shared.Models
{
    // All FK etc. not seen here are defined in the DbContext 'OnModelCreating' method.
    [Table(nameof(Person))]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(6)]
        public string EmployeeNum { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(13)]
        public string IdNumber { get; set; }
    }
}
