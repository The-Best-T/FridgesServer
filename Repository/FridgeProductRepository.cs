using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public FridgeProduct GetProductForFridge(Guid fridgeId, Guid productId, bool trackChanges)
        {
            return FindByCondition(fp =>
                    fp.FridgeId.Equals(fridgeId) &&
                    fp.ProductId.Equals(productId), trackChanges)
                   .SingleOrDefault();
        }

        public IEnumerable<FridgeProduct> GetProductsForFridge(Guid fridgeId, bool trakChanges)
        {
            return FindByCondition(fp => fp.FridgeId.Equals(fridgeId), trakChanges)
                   .OrderBy(fp => fp.Quantity)
                   .ToList();
        }
    }
}
