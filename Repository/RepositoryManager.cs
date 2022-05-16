using Contracts;
using Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private IProductRepository _productRepository;
        private IFridgeRepository _fridgeRepository;
        private IFridgeModelRepository _fridgeModelRepository;
        private IFridgeProductRepository _fridgeProductRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _repositoryContext = context;
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);
                return _productRepository;
            }
        }

        public IFridgeRepository Fridge
        {
            get
            {
                if (_fridgeRepository == null)
                    _fridgeRepository = new FridgeRepository(_repositoryContext);
                return _fridgeRepository;
            }
        }
        public IFridgeModelRepository FridgeModel
        {
            get
            {
                if (_fridgeModelRepository == null)
                    _fridgeModelRepository = new FridgeModelRepository(_repositoryContext);
                return _fridgeModelRepository;
            }
        }
        public IFridgeProductRepository FridgeProduct
        {
            get
            {
                if (_fridgeProductRepository == null)
                    _fridgeProductRepository = new FridgeProductRepository(_repositoryContext);
                return _fridgeProductRepository;
            }
        }
        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }

        public async Task StoredProcedureWithoutParamasAsync(string name)
        {
            await _repositoryContext.Database.ExecuteSqlRawAsync("EXECUTE "+name);
        }
    }
}
