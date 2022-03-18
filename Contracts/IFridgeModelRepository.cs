using Entities.Models;
using System;
using System.Collections.Generic;
namespace Contracts
{
    public interface IFridgeModelRepository
    {
        IEnumerable<FridgeModel> GetAllFridgeModels(bool trackChanges);
        FridgeModel GetFridgeModel(Guid id, bool trackChanges);
        void CreateFridgeModel(FridgeModel fridgeModel);

    }
}
