using System;
using System.Globalization;
using AcmeSoft.Mvc.Models;
using AcmeSoft.Mvc.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;

namespace AcmeSoft.Mvc.Mapping
{
    public static class AutoMapperConfig
    {
        public static void BuildMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<PersonEmployeeDto, PersonViewModel>()
                    .ForMember(dest => dest.BirthDate, opt => opt.ResolveUsing(src => src.BirthDate.ToString(AppConstants.DefaultDateFormat)))
                    .ForMember(dest => dest.EmployedDate, opt => opt.ResolveUsing(src => src.EmployedDate?.ToString(AppConstants.DefaultDateFormat)))
                    .ForMember(dest => dest.TerminatedDate, opt => opt.ResolveUsing(src => src.TerminatedDate?.ToString(AppConstants.DefaultDateFormat)));

                cfg.CreateMap<Person, PersonViewModel>(MemberList.None)
                    .ForMember(dest => dest.BirthDate, opt => opt.ResolveUsing(src => src.BirthDate.ToString(AppConstants.DefaultDateFormat)));

                cfg.CreateMap<PersonViewModel, Person>(MemberList.None)
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.BirthDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)));



                cfg.CreateMap<Employee, PersonViewModel>()
                    .ForMember(dest => dest.EmployedDate, opt => opt.ResolveUsing(src => src.EmployedDate.ToString(AppConstants.DefaultDateFormat)))
                    .ForMember(dest => dest.TerminatedDate, opt => opt.ResolveUsing(src => src.TerminatedDate?.ToString(AppConstants.DefaultDateFormat)));

                cfg.CreateMap<PersonViewModel, Employee>()
                    .ForMember(dest => dest.EmployedDate,
                        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)));
            });
        }
    }
}
