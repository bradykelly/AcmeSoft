using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeSoft.Mvc.ViewModels
{
    public class CombinedIndexViewModel
    {
        public PersonIndexViewModel Persons { get; set; } = new PersonIndexViewModel();

        public EmployeeIndexViewModel Employees { get; set; } = new EmployeeIndexViewModel();
    }
}
