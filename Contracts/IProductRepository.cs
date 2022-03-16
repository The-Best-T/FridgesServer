using Entities.Models;
using System.Collections.Generic;
using System;
namespace Contracts
{
    public interface IProductRepository 
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);
        Product GetProduct(Guid id, bool trackChanges);
        void CreateProduct(Product product);
    }
}
