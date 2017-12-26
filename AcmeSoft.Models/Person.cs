using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcmeSoft.Models
{
    // All FK and other relationships etc. not seen here are in the DbContext 'OnModelCreating' method.
    [Table(nameof(Person))]
    public class Person
    {
        [Key]
        public int PersonId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        public string FirstName { get; set; }

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
    }
}
