using System;
using System.Globalization;
using AcmeSoft.Gui.Models;
using AcmeSoft.Gui.ViewModels;
using AcmeSoft.Shared.Models;
using AutoMapper;

namespace AcmeSoft.Gui.Mapping
{
    public static class AutoMapperConfig
    {
        public static void BuildMaps()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Person, PersonViewModel>(MemberList.None)
                    .ForMember(dest => dest.BirthDate, opt => opt.ResolveUsing(src =>
                    {
                        if (src.BirthDate == DateTime.MinValue)
                        {
                            return null;
                        }
                        return src.BirthDate.ToString(AppConstants.DefaultDateFormat);
                    }));

                cfg.CreateMap<PersonViewModel, Person>(MemberList.None)
                    .ForMember(dest => dest.BirthDate,
                        opt => opt.ResolveUsing(src =>
                        {
                            if (src.BirthDate == null)
                            {
                                return DateTime.MinValue;
                            }
                            return DateTime.ParseExact(src.BirthDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture);
                        }));

                cfg.CreateMap<Employment, EmploymentViewModel>();

                ////cfg.CreateMap<Employee, PersonViewModel>()
                ////    .ForMember(dest => dest.EmployedDate, opt => opt.ResolveUsing(src => src.EmployedDate.ToString(AppConstants.DefaultDateFormat)))
                ////    .ForMember(dest => dest.TerminatedDate, opt => opt.ResolveUsing(src => src.TerminatedDate?.ToString(AppConstants.DefaultDateFormat)));

                ////cfg.CreateMap<PersonViewModel, Employee>()
                ////    .ForMember(dest => dest.EmployedDate,
                ////        opt => opt.ResolveUsing(src => DateTime.ParseExact(src.EmployedDate, AppConstants.DefaultDateFormat, CultureInfo.InvariantCulture)));
            });
        }
    }
}
