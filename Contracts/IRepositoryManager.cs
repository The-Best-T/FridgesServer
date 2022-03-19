namespace Contracts
{
    public interface IRepositoryManager
    {
        IProductRepository Product { get; }
        IFridgeRepository Fridge { get; }
        IFridgeModelRepository FridgeModel { get; }
        IFridgeProductRepository FridgeProduct { get; }
        void Save();
    }

}
