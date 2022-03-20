using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<FridgeModel>> GetFridgeModelsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges)
                        .OrderBy(fm => fm.Name)
                        .ToListAsync();
        }

        public async Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(fm => fm.Id.Equals(id), trackChanges)
                        .SingleOrDefaultAsync();
        }
    }
}
