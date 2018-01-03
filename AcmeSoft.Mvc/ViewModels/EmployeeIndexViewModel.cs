﻿using System.Collections.Generic;
using AcmeSoft.Mvc.Models;

namespace AcmeSoft.Mvc.ViewModels
{
    public class EmployeeIndexViewModel
    {
        public ViewModelPurpose ModelPurpose { get; set; }

        public string ViewHeading => "Employee Index";

        public IEnumerable<EmployeeViewModel> Items { get; set; } = new List<EmployeeViewModel>();

        public EmployeeViewModel HeaderItem { get; }
    }
}