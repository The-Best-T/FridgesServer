using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateProduct(Product product)
        {
            Create(product);
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return FindAll(trackChanges)
                   .OrderBy(p => p.Name)
                   .ToList();
        }

        public Product GetProduct(Guid id, bool trackChanges)
        {
            return FindByCondition(p => p.Id.Equals(id), trackChanges)
                   .SingleOrDefault();
        }
    }
}
