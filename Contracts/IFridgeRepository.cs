using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeRepository
    {
        Task<IEnumerable<Fridge>> GetFridgesForModelAsync(Guid fridgeModelId, bool trackChanges);
        Task<Fridge> GetFridgeForModelAsync(Guid firdgeModelId, Guid id, bool trackChanges);
        void CreateFridgeForModel(Guid fridgeModelId, Fridge fridge);
        void DeleteFridge(Fridge fridge);

    }
}
