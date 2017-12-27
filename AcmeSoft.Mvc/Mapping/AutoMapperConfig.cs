using AcmeSoft.Models;
using AcmeSoft.Mvc.ViewModels;
using AutoMapper;

namespace AcmeSoft.Mvc.Mapping
{
    public static class AutoMapperConfig
    {
        public static void BuildMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, EmployeeViewModel>().ReverseMap();
                cfg.CreateMap<Employee, EmployeeViewModel>().ReverseMap();
            });
        }
    }
}
