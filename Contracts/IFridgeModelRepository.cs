using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeModelRepository
    {
        Task<PagedList<FridgeModel>> GetFridgeModelsAsync(FridgeModelParameters parameters, bool trackChanges);
        Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges);
        void CreateFridgeModel(FridgeModel fridgeModel);
        void DeleteFridgeModel(FridgeModel fridgeModel);

    }
}
