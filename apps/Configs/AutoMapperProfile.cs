
using AutoMapper;
using apps.Models.Request;

namespace apps.Configs
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FindPromoRequest, EngineRequest>()
                .ForMember(x => x.TransDate, opt => opt.MapFrom(q => q.TransDate!.Replace("/", "-")));
        }
    }
}
