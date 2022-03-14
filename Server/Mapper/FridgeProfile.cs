using Entities.Models;
using Entities.DTO.Fridge;
namespace AutoMapper
{
    public class FridgeProfile : Profile
    {
        public FridgeProfile()
        {
            CreateMap<Fridge, FridgeDTO>()
                .ForMember(f => f.Model,
                           opt => opt.MapFrom(x => x.Model));

        }
    }
}
