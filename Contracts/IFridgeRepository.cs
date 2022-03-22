using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeRepository
    {
        Task<PagedList<Fridge>> GetFridgesForModelAsync(Guid fridgeModelId, FridgeParameters parameters, bool trackChanges);
        Task<Fridge> GetFridgeForModelAsync(Guid firdgeModelId, Guid id, bool trackChanges);
        void CreateFridgeForModel(Guid fridgeModelId, Fridge fridge);
        void DeleteFridge(Fridge fridge);

    }
}
