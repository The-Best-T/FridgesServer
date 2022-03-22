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

        public async Task<PagedList<FridgeModel>> GetFridgeModelsAsync(FridgeModelParameters parameters, bool trackChanges)
        {
            var fridgeModels = await FindAll(trackChanges)
                .OrderBy(fm => fm.Name)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<FridgeModel>(fridgeModels, count, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<FridgeModel> GetFridgeModelAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(fm => fm.Id.Equals(id), trackChanges)
                        .SingleOrDefaultAsync();
        }
    }
}
