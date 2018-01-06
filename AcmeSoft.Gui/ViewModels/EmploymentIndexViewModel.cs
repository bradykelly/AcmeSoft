using System.Collections.Generic;
using AcmeSoft.Gui.Models;

namespace AcmeSoft.Gui.ViewModels
{
    public class EmploymentIndexViewModel
    {
        // NB Use base class.
        public ViewModelPurpose ModelPurpose { get; set; }

        public int PersonId { get; set; }

        public IEnumerable<EmploymentViewModel> Items { get; set; } = new List<EmploymentViewModel>();

        public EmploymentViewModel HeaderItem { get; }
    }
}