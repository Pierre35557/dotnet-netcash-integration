using AutoMapper;
using Netcash.Data.Requests;
using Netcash.DTO.Requests;
using NIWS;

namespace Netcash.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateMandateRequest, AddMandateRequest>()
                .ForMember(dest => dest.DebitFrequency, opt => opt.MapFrom(src => (MandateOptionsMandateDebitFrequency)src.DebitFrequency))
                .ForMember(dest => dest.PublicHolidayOption, opt => opt.MapFrom(src => (MandateOptionsMandatePublicHolidayOption?)src.PublicHolidayOption));
        }
    }
}
