using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    internal class FridgeModelRepository : RepositoryBase<FridgeModel>, IFridgeModelRepository
    {
        public FridgeModelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<FridgeModel> GetAllFridgeModels(bool trackChanges)
        {
            return FindAll(trackChanges)
                   .ToList();
        }

        public FridgeModel GetFridgeModel(Guid id, bool trackChanges)
        {
            return FindByCondition(fm => fm.Id.Equals(id), trackChanges)
                   .SingleOrDefault();
        }
    }
}
