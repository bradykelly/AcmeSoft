using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AcmeSoft.Gui.Models;

namespace AcmeSoft.Gui.ViewModels
{
    public class PersonIndexViewModel
    {
        public ViewModelPurpose ModelPurpose { get; set; }

        public string ViewHeading => "Employee Index";

        public IEnumerable<PersonViewModel> Items { get; set; } = new List<PersonViewModel>();

        public PersonViewModel HeaderItem { get; }
    }
}