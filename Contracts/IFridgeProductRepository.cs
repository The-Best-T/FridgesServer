using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeProductRepository
    {
        Task<PagedList<FridgeProduct>> GetProductsForFridgeAsync(Guid fridgeId, FridgeProductParameters parameters, bool trakChanges);
        Task<FridgeProduct> GetProductForFridgeAsync(Guid fridgeId, Guid productId, bool trackChanges);
        void AddProductInFridge(Guid fridgeId, FridgeProduct fridgeProduct);
        void DeleteProductFromFridge(FridgeProduct fridgeProduct);
    }
}
