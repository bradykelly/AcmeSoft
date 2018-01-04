using System;
using System.ComponentModel.DataAnnotations;
using AcmeSoft.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.ViewModels
{
    public class PersonEmployeeViewModel: BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(128)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name = "Id Number")]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(16)]
        [Display(Name = "Employee No.")]
        public string EmployeeNum { get; set; }

        [Required]
        [Display(Name = "Date Employed")]
        public string EmployedDate { get; set; }

        [Display(Name = "Date Terminated")]
        public string TerminatedDate { get; set; }

        public DateTime? Archived { get; set; }

        public override string ViewHeading => "PersonEmployee";
    }
}
