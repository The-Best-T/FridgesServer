using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetProductAsync(Guid id, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
