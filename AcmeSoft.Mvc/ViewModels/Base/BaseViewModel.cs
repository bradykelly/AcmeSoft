using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Models;

namespace AcmeSoft.Mvc.ViewModels.Base
{
    public abstract class BaseViewModel
    {
        public ViewModelPurpose ModelPurpose { get; set; }

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
    }
}
