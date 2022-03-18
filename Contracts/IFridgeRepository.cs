using Entities.Models;
using System;
using System.Collections.Generic;
namespace Contracts
{
    public interface IFridgeRepository
    {
        IEnumerable<Fridge> GetFridgesForModel(Guid fridgeModelId, bool trackChanges);
        Fridge GetFridgeForModel(Guid firdgeModelId, Guid id, bool trackChanges);
        void CreateFridgeForModel(Guid fridgeModelId, Fridge fridge);
        IEnumerable<FridgeProduct> GetProductsForFridge(Guid id);
        FridgeProduct GetProductForFridge(Guid id, Guid productId);
        void AddFridgeProduct(Guid fridgeId, FridgeProduct fridgeProduct);


    }
}
