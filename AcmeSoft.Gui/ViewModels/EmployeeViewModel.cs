using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeSoft.Gui.ViewModels.Base;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.ViewModels
{
    public class EmployeeViewModel: BaseViewModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }

        [ReadOnly(true)]
        public string LastName { get; set; }

        [ReadOnly(true)]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(16)]
        [Display(Name = "Employee Number")]
        public string EmployeeNum { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Employed")]
        public DateTime EmployedDate { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Terminated")]
        public DateTime? TerminatedDate { get; set; }

        public Person Person { get; set; }
    }
}
