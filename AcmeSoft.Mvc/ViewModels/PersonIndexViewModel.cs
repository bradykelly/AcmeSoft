using System.Collections.Generic;
using AcmeSoft.Mvc.Models;

namespace AcmeSoft.Mvc.ViewModels
{
    public class PersonIndexViewModel
    {
        public ViewModelPurpose ModelPurpose { get; set; }

        public string ViewHeading => "Employee Index";

        public IEnumerable<PersonViewModel> Items { get; set; } = new List<PersonViewModel>();

        public PersonViewModel HeaderItem { get; }
    }
}