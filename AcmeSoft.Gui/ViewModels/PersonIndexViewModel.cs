using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.ViewModels.Base;

namespace AcmeSoft.Gui.ViewModels
{
    public class PersonIndexViewModel: BaseViewModel
    {
        public override string ViewHeading => "Employee Index";

        public IEnumerable<PersonViewModel> Items { get; set; } = new List<PersonViewModel>();

        public PersonViewModel HeaderItem { get; }
    }
}