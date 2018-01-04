using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeViewModel: BaseViewModel
    {
        [Key]
        public int EmployeeId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PersonId { get; set; }

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
