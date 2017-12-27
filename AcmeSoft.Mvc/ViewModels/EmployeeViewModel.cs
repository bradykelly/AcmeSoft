using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeViewModel: BaseViewModel
    {
        // NB Remove Key after scaffolding.
        [Key]
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
        public DateTime? BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(16)]
        [Display(Name = "Employee No.")]
        public string EmployeeNum { get; set; }

        [Required]
        [Display(Name = "Date Employed")]
        public DateTime? EmployedDate { get; set; }

        [Display(Name = "Date Terminated")]
        public DateTime? TerminatedDate { get; set; }
    }
}
