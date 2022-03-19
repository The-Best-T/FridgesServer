using Entities.Models;
using System;
using System.Collections.Generic;
namespace Contracts
{
    public interface IFridgeProductRepository
    {
        IEnumerable<FridgeProduct> GetProductsForFridge(Guid fridgeId, bool trakChanges);
        FridgeProduct GetProductForFridge(Guid fridgeId, Guid productId, bool trackChanges);
        void AddProductInFridge(Guid fridgeId, FridgeProduct fridgeProduct);
        void DeleteProductFromFridge(FridgeProduct fridgeProduct);
    }
}
