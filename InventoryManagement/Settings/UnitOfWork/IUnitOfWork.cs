using InventoryManagement.Contracts.Repository;

namespace InventoryManagement.Settings.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        IProductRepository Products { get; }
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        IOrderItemRepository OrderItems { get; }
        IInventoryMovementRepository InventoryMovements { get; }

        void Begin();
        void Commit();
        void Rollback();
    }
}
