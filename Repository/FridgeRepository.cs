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
        public Fridge GetFridgeForModel(Guid modelId, Guid id, bool trackChanges)
        {
            return FindByCondition(f => f.ModelId.Equals(modelId) && f.Id.Equals(id), trackChanges)
                   .SingleOrDefault();              
        }

        public IEnumerable<Fridge> GetFridgesForModel(Guid modelId, bool trackChanges)
        {
            return FindByCondition(f => f.ModelId.Equals(modelId), trackChanges)
                   .OrderBy(f => f.Name)
                   .ToList();
        }
    }
}
