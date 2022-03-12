namespace Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        IFridgeRepository Fridge { get; }
        IFridgeModelRepository FridgeModel { get; }
        void Save();
    }

}
