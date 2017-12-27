using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels.Base;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeIndexViewModel : BaseViewModel
    {
        public override string ViewHeading => "Employee Index";

        public IEnumerable<EmployeeViewModel> Items { get; set; }

        public EmployeeViewModel HeaderItem { get; }
    }
}
