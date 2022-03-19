using Entities.Models;
using System;
using System.Collections.Generic;
namespace Contracts
{
    public interface IFridgeModelRepository
    {
        IEnumerable<FridgeModel> GetFridgeModels(bool trackChanges);
        FridgeModel GetFridgeModel(Guid id, bool trackChanges);
        void CreateFridgeModel(FridgeModel fridgeModel);
        void DeleteFridgeModel(FridgeModel fridgeModel);

    }
}
