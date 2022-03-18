using Entities.Models;
using Entities.DTO.FridgeModel;
namespace AutoMapper
{
    public class FridgeModelProfile : Profile
    {
        public FridgeModelProfile()
        {
            CreateMap<FridgeModel, FridgeModelDTO>();
            CreateMap<FridgeModelForCreationDTO, FridgeModel>();
        }
    }
}
