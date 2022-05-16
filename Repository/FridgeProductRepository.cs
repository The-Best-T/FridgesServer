using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
namespace Repository
{
    public class FridgeProductRepository : RepositoryBase<FridgeProduct>, IFridgeProductRepository
    {
        public FridgeProductRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void AddProductInFridge(Guid fridgeId, FridgeProduct fridgeProduct)
        {
            fridgeProduct.FridgeId = fridgeId;
            Create(fridgeProduct);
        }

        public void DeleteProductFromFridge(FridgeProduct fridgeProduct)
        {
            Delete(fridgeProduct);
        }

        public async Task<FridgeProduct> GetProductForFridgeAsync(Guid fridgeId, Guid productId, bool trackChanges)
        {
            return await FindByCondition(fp =>
                fp.FridgeId.Equals(fridgeId) &&
                fp.ProductId.Equals(productId), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<FridgeProduct>> GetProductsForFridgeAsync(Guid fridgeId, FridgeProductParameters parameters, bool trakChanges)
        {
            var fridgeProducts = await FindByCondition(fp => fp.FridgeId.Equals(fridgeId), trakChanges)
                .OrderBy(f => f.Quantity)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(f => f.FridgeId.Equals(fridgeId), trackChanges: false)
                .CountAsync();

            return new PagedList<FridgeProduct>(fridgeProducts, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
