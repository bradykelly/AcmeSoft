using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.ViewModels.Base;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.ViewModels
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string BirthDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(13)]
        [Display(Name = "Id Number")]
        public string IdNumber { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Emp. Number")]
        public string EmployeeNum { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Employed")]
        public string EmployedDate { get; set; }

        [ReadOnly(true)]
        [Display(Name = "Terminated")]
        public string TerminatedDate { get; set; }

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
                        return "Employees";
                }
            }
        }
    }
}
