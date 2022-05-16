using Entities.Models;
using Entities.Dto.Fridge;
namespace AutoMapper
{
    public class FridgeProfile : Profile
    {
        public FridgeProfile()
        {
            CreateMap<Fridge, FridgeDto>();
            CreateMap<FridgeForCreationDto, Fridge>();
            CreateMap<FridgeForUpdateDto, Fridge>();
        }
    }
}
