using Entities.Models;
using Entities.DTO.Fridge;
namespace AutoMapper
{
    public class FridgeProfile : Profile
    {
        public FridgeProfile()
        {
            CreateMap<Fridge, FridgeDTO>();
            CreateMap<FridgeForCreationDTO, Fridge>();
            CreateMap<FridgeForUpdateDTO, Fridge>();
        }
    }
}
