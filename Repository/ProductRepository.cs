using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                        .OrderBy(p => p.Name)
                        .ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(p => p.Id.Equals(id), trackChanges)
                        .SingleOrDefaultAsync();
        }
    }
}
