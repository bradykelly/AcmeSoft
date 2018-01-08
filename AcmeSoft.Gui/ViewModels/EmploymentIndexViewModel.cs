using System.Collections.Generic;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.ViewModels.Base;

namespace AcmeSoft.Gui.ViewModels
{
    public class EmploymentIndexViewModel: BaseViewModel
    {
        public int PersonId { get; set; }

        public IEnumerable<EmploymentViewModel> Items { get; set; } = new List<EmploymentViewModel>();

        public EmploymentViewModel HeaderItem { get; }
    }
}