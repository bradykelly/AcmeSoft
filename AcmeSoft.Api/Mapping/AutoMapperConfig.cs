using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AcmeSoft.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;

namespace AcmeSoft.Api.Mapping
{
    public static class AutoMapperConfig
    {
        public static void BuildMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, EmployeeViewModel>();
            });
        }
    }
}
