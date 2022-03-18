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

        public void AddFridgeProduct(Guid fridgeId, FridgeProduct fridgeProduct)
        {
            fridgeProduct.FridgeId= fridgeId;
            FindByCondition(f=>f.Id.Equals(fridgeId),trackChanges:true)
            .SingleOrDefault()
            .FridgeProducts.Add(fridgeProduct);
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

        public void DeleteFridgeProduct(FridgeProduct fridgeProduct)
        {
            FindByCondition(f => f.Id.Equals(fridgeProduct.FridgeId), trackChanges: true)
           .SingleOrDefault()
           .FridgeProducts.Remove(fridgeProduct);
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

        public FridgeProduct GetProductForFridge(Guid id, Guid productId)
        {
            return FindByCondition(f => f.Id.Equals(id), trackChanges: true)
                   .SingleOrDefault()
                   .FridgeProducts
                   .SingleOrDefault(fp=>fp.ProductId.Equals(productId));
        }

        public IEnumerable<FridgeProduct> GetProductsForFridge(Guid id)
        {
            return FindByCondition(f => f.Id.Equals(id), trackChanges: true)
                   .SingleOrDefault()
                   .FridgeProducts;
        }
    }
}
