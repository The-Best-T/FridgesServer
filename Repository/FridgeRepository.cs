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

        public void DeleteFridge(Fridge fridge)
        {
            Delete(fridge);
        }

        public async Task<Fridge> GetFridgeForModelAsync(Guid fridgeModelId, Guid id, bool trackChanges)
        {
            return await FindByCondition(f => f.ModelId.Equals(fridgeModelId) && f.Id.Equals(id), trackChanges)
                        .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Fridge>> GetFridgesForModelAsync(Guid fridgeModelId, bool trackChanges)
        {
            return await FindByCondition(f => f.ModelId.Equals(fridgeModelId), trackChanges)
                        .OrderBy(f => f.Name)
                        .ToListAsync();
        }
    }
}
