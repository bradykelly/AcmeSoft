using System.Collections.Generic;
using AcmeSoft.Mvc.Models;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeAllIndexViewModel 
    {
        public ViewModelPurpose ModelPurpose { get; set; }

        public string ViewHeading => "Employee Index";

        public IEnumerable<EmployeeAllViewModel> Items { get; set; }

        public EmployeeAllViewModel HeaderItem { get; }
    }
}
