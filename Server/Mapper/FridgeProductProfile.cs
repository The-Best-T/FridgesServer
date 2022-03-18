using Entities.Models;
using Entities.DTO.Product;
namespace AutoMapper
{
    public class FridgeProductProfile:Profile
    {
        public FridgeProductProfile()
        {
            CreateMap<FridgeProduct, FridgeProductDTO>()
                .ForMember(fp => fp.ProductName,
                           opt => opt.MapFrom(m => m.Product.Name));
            CreateMap<FridgeProductToCreationDTO, FridgeProduct>();
        }
    }
}
