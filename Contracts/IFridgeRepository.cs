using Entities.Models;
using System;
using System.Collections.Generic;
namespace Contracts
{
    public interface IFridgeRepository
    {
        IEnumerable<Fridge> GetFridgesForModel(Guid modelId, bool trackChanges);
        Fridge GetFridgeForModel(Guid modelId,Guid id, bool trackChanges);

    }
}
