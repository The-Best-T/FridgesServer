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

        public IEnumerable<Fridge> GetAllFridges(bool trackChanges)
        {
            return FindAll(trackChanges)
                .OrderBy(f => f.Name)
                .ToList();
        }

        public Fridge GetFridge(Guid id, bool trackChanges)
        {
            return FindByCondition(f => f.Id.Equals(id), trackChanges)
                   .SingleOrDefault();
        }
    }
}
