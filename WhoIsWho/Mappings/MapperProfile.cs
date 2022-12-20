using AutoMapper;
using WhoIsWho.Models;
using WhoIsWho.Models.Entities;

namespace WhoIsWho.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<PostBookingModel, Booking>()
                .ForMember(p => p.User, map => map.Ignore());
        }
    }
}
