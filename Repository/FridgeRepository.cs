using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    public class FridgeRepository : RepositoryBase<Fridge>, IFridgeRepository
    {
        public FridgeRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateFridgeForModel(Guid fridgeModelId, Fridge fridge)
        {
            fridge.ModelId = fridgeModelId;
            Create(fridge);
        }

        public Fridge GetFridgeForModel(Guid fridgeModelId, Guid id, bool trackChanges)
        {
            return FindByCondition(f => f.ModelId.Equals(fridgeModelId) && f.Id.Equals(id), trackChanges)
                   .SingleOrDefault();              
        }

        public IEnumerable<Fridge> GetFridgesForModel(Guid fridgeModelId, bool trackChanges)
        {
            return FindByCondition(f => f.ModelId.Equals(fridgeModelId), trackChanges)
                   .OrderBy(f => f.Name)
                   .ToList();
        }
    }
}
