using Entities.Models;
using Entities.DTO.Product;
namespace AutoMapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductForCreationDTO, Product>();
        }
    }
}
