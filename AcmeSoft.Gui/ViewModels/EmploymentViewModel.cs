using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AcmeSoft.Gui.Models;
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

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Employed")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string EmployedDate { get; set; }

        [Display(Name = "Terminated")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string TerminatedDate { get; set; }

        public Person Person { get; set; }

        [ScaffoldColumn(false)]
        public override string ViewHeading
        {
            get
            {
                switch (ModelPurpose)
                {
                    case ViewModelPurpose.Create:
                        return "Create Employment";
                    case ViewModelPurpose.Index:
                        return "Employment Records";
                    case ViewModelPurpose.View:
                        return "Employment Details";
                    case ViewModelPurpose.Edit:
                        return "Edit Employment";
                    case ViewModelPurpose.Delete:
                        return "Delete Employment";
                    default:
                        return "Employment Records";
                }
            }
        }
    }
}
