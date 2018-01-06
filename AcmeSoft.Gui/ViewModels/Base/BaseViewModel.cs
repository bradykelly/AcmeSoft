using System.ComponentModel.DataAnnotations;
using AcmeSoft.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace AcmeSoft.Gui.ViewModels.Base
{
    public class BaseViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public ViewModelPurpose ModelPurpose { get; set; }

        [ScaffoldColumn(false)]
        public virtual string ViewHeading => "Employees";

        [ScaffoldColumn(false)]
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

        public virtual string ButtonText
        {
            get
            {
                switch (ModelPurpose)
                {
                    case ViewModelPurpose.Create:
                        return "Create";
                    case ViewModelPurpose.View:
                        return "Update";
                    case ViewModelPurpose.Edit:
                        return "Update";
                    case ViewModelPurpose.Delete:
                        return "Delete";
                    default:
                        return "Submit";
                }
            }
        }
    }
}