using System.Collections.Generic;
using AcmeSoft.Mvc.Models;

namespace AcmeSoft.Mvc.ViewModels
{
    public class PersEmpIndexViewModel: BaseViewModel
    {
        public override string ViewHeading => "Employee Index";

        public IEnumerable<PersonEmployeeViewModel> Items { get; set; }

        public PersonEmployeeViewModel HeaderItem { get; }
    }
}
