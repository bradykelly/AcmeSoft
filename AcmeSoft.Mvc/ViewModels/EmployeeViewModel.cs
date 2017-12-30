using System.ComponentModel.DataAnnotations;
using AcmeSoft.Mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeViewModel
    {
        public ViewModelPurpose ModelPurpose { get; set; }

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

        public virtual string ViewHeading
        {
            get
            {
                switch (ModelPurpose)
                {
                    case ViewModelPurpose.Create:
                        return "Create New Employee";
                    case ViewModelPurpose.View:
                        return "View Employee Details";
                    case ViewModelPurpose.Edit:
                        return "Edit Employee Details";
                    case ViewModelPurpose.Delete:
                        return "Delete Employee";
                    default:
                        return "Employee Index";
                }
            }
        }

        public virtual string FormAction
        {
            get
            {
                switch (ModelPurpose)
                {
                    case ViewModelPurpose.Create:
                        return "Create";
                    case ViewModelPurpose.View:
                        return "Edit";
                    case ViewModelPurpose.Edit:
                        return "Edit";
                    case ViewModelPurpose.Delete:
                        return "Delete";
                    default:
                        return null;
                }
            }
        }
    }
}
