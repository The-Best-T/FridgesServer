using Entities.Models;
using Entities.Dto.FridgeProduct;
namespace AutoMapper
{
    public class FridgeProductProfile:Profile
    {
        public FridgeProductProfile()
        {
            CreateMap<FridgeProduct, FridgeProductDto>()
                .ForMember(fp => fp.ProductName,
                           opt => opt.MapFrom(m => m.Product.Name));
            CreateMap<FridgeProductForCreationDto, FridgeProduct>();
            CreateMap<FridgeProductForUpdateDto, FridgeProduct>();
        }
    }
}
