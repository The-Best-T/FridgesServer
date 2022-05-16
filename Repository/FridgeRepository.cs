using Contracts;
using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
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

        public void CreateFridge(Fridge fridge)
        {
            Create(fridge);
        }

        public void DeleteFridge(Fridge fridge)
        {
            Delete(fridge);
        }

        public async Task<Fridge> GetFridgeAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(f => f.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<Fridge>> GetFridgesAsync(FridgeParameters parameters, bool trackChanges)
        {
            var fridges = await FindAll(trackChanges)
                .OrderBy(f => f.Name)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            var count = await FindAll(trackChanges: false)
                .CountAsync();

            return new PagedList<Fridge>(fridges, count, parameters.PageNumber, parameters.PageSize);
        }
    }
}
