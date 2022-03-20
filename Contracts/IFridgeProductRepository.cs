using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeProductRepository
    {
        Task<IEnumerable<FridgeProduct>> GetProductsForFridgeAsync(Guid fridgeId, bool trakChanges);
        Task<FridgeProduct> GetProductForFridgeAsync(Guid fridgeId, Guid productId, bool trackChanges);
        void AddProductInFridge(Guid fridgeId, FridgeProduct fridgeProduct);
        void DeleteProductFromFridge(FridgeProduct fridgeProduct);
    }
}
