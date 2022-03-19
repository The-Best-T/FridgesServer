using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Repository
{
    public class FridgeModelRepository : RepositoryBase<FridgeModel>, IFridgeModelRepository
    {
        public FridgeModelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public void CreateFridgeModel(FridgeModel fridgeModel)
        {
            Create(fridgeModel);
        }

        public void DeleteFridgeModel(FridgeModel fridgeModel)
        {
            Delete(fridgeModel);
        }

        public IEnumerable<FridgeModel> GetAllFridgeModels(bool trackChanges)
        {
            return FindAll(trackChanges)
                   .OrderBy(fm => fm.Name)
                   .ToList();
        }

        public FridgeModel GetFridgeModel(Guid id, bool trackChanges)
        {
            return FindByCondition(fm => fm.Id.Equals(id), trackChanges)
                   .SingleOrDefault();
        }
    }
}
