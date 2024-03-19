
using AutoMapper;
using apps.Engine.Models.Request;
using apps.Engine.Models.Dto;

namespace apps.Engine.Helpers
{
    public class EngineMapper : Profile
    {
        public EngineMapper()
        {
            CreateMap<FindPromoRequest, EngineParamsDto>()
                .ForMember(x => x.TransDate, opt => opt.MapFrom(q => q.TransDate!.Replace("/", "-")));
        }
    }
}
