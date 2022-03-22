using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetAllProductsAsync(ProductParameters parameters, bool trackChanges);
        Task<Product> GetProductAsync(Guid id, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
