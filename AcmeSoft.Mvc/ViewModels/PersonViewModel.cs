using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.ViewModels
{
    public class PersonViewModel: BaseViewModel
    {        
        [Key]
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

        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "date")]
        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(13)]
        [Display(Name = "Id Number")]
        public string IdNumber { get; set; }

        [ScaffoldColumn(false)]
        public override string ViewHeading
        {
            get
            {
                switch (ModelPurpose)
                {
                    case ViewModelPurpose.Create:
                        return "Create Employee";
                    case ViewModelPurpose.Index:
                        return "Employee Index";
                    case ViewModelPurpose.View:
                        return "Employee Details"; 
                    case ViewModelPurpose.Edit:
                        return "Edit Employee"; 
                    case ViewModelPurpose.Delete:
                        return "Delete Employee"; 
                    default:
                        return string.Empty;
                }
            }
        }
    }
}
