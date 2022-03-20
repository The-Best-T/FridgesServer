using Entities.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Contracts
{
    public interface IFridgeModelRepository
    {
        Task<IEnumerable<FridgeModel>> GetFridgeModelsAsync(bool trackChanges);
        Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges);
        void CreateFridgeModel(FridgeModel fridgeModel);
        void DeleteFridgeModel(FridgeModel fridgeModel);

    }
}
