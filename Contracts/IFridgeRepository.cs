using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeRepository
    {
        Task<PagedList<Fridge>> GetFridgesAsync(FridgeParameters parameters, bool trackChanges);
        Task<Fridge> GetFridgeAsync(Guid id, bool trackChanges);
        void CreateFridge(Fridge fridge);
        void DeleteFridge(Fridge fridge);

    }
}
