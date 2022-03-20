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

        public async Task<IEnumerable<FridgeProduct>> GetProductsForFridgeAsync(Guid fridgeId, bool trakChanges)
        {
            return await FindByCondition(fp => fp.FridgeId.Equals(fridgeId), trakChanges)
                        .OrderBy(fp => fp.Quantity)
                        .ToListAsync();
        }
    }
}
