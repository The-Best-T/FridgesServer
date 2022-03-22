using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.RequestFeatures;
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

        public async Task<PagedList<Product>> GetAllProductsAsync(ProductParameters parameters,bool trackChanges)
        {
            var products = await FindAll(trackChanges)
                .OrderBy(p => p.Name)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            var count = await FindAll(trackChanges:false).CountAsync();

            return new PagedList<Product>(products, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<Product> GetProductAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(p => p.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }
    }
}
