using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeSoft.Gui.ViewModels.Base;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.ViewModels
{
    public class EmploymentViewModel: BaseViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(100)]
        public string Position { get; set; }

        [ReadOnly(true)]
        public string LastName { get; set; }

        [ReadOnly(true)]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(16)]
        [Display(Name = "Employee Number")]
        public string EmployeeNum { get; set; }

        [Display(Name = "Employed")]
        public string EmployedDate { get; set; }

        [Display(Name = "Terminated")]
        public string TerminatedDate { get; set; }

        public Person Person { get; set; }
    }
}
