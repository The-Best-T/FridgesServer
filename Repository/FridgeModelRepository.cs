using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    internal class FridgeModelRepository : RepositoryBase<FridgeModel>, IFridgeModelRepository
    {
        public FridgeModelRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
