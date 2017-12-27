using System;
using System.Globalization;
using AcmeSoft.Models;
using AcmeSoft.Mvc.Models;
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
                cfg.CreateMap<Person, EmployeeViewModel>()
                    .ForMember(dest => dest.BirthDate, opt => opt.ResolveUsing(src => src.BirthDate.ToString(AppConstants.DefaultDateFormat)));

                cfg.CreateMap<EmployeeViewModel, Person>()
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.BirthDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)));

                cfg.CreateMap<Employee, EmployeeViewModel>()
                    .ForMember(dest => dest.EmployedDate, opt => opt.ResolveUsing(src => src.EmployedDate.ToString(AppConstants.DefaultDateFormat)))
                    .ForMember(dest => dest.TerminatedDate, opt => opt.ResolveUsing(src => src.TerminatedDate?.ToString(AppConstants.DefaultDateFormat)));

                cfg.CreateMap<EmployeeViewModel, Employee>()
                    .ForMember(dest => dest.EmployedDate,
                        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)))
                    .ForMember(dest => dest.TerminatedDate,
                        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.TerminatedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)));
            });
        }
    }
}
