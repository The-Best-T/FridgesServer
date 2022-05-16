using Entities.Models;
using Entities.Dto.FridgeModel;
namespace AutoMapper
{
    public class FridgeModelProfile : Profile
    {
        public FridgeModelProfile()
        {
            CreateMap<FridgeModel, FridgeModelDto>();
            CreateMap<FridgeModelForCreationDto, FridgeModel>();
            CreateMap<FridgeModelForUpdateDto, FridgeModel>();
        }
    }
}
