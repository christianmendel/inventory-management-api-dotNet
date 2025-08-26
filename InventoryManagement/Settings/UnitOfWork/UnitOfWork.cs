using InventoryManagement.Contracts.Repository;
using InventoryManagement.Models;
using InventoryManagement.Repository;
using InventoryManagement.Settings.Base;
using System.Data;

namespace InventoryManagement.Settings.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction? _transaction;

        public ICategoryRepository Categories { get; }
        public IProductRepository Products { get; }
        public ICustomerRepository Customers { get; }
        public IOrderRepository Orders { get; }
        public IOrderItemRepository OrderItems { get; }
        public IInventoryMovementRepository InventoryMovements { get; }

        public UnitOfWork(
            IDbConnection connection,
            ICategoryRepository categories,
            IProductRepository products,
            ICustomerRepository customers,
            IOrderRepository orders,
            IOrderItemRepository orderItems,
            IInventoryMovementRepository inventoryMovements)
        {
            _connection = connection;

            Categories = categories;
            Products = products;
            Customers = customers;
            Orders = orders;
            OrderItems = orderItems;
            InventoryMovements = inventoryMovements;
        }

        public void Begin()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            _transaction = _connection.BeginTransaction();

            (Categories as RepositoryBase)?.SetTransaction(_transaction);
            (Products as RepositoryBase)?.SetTransaction(_transaction);
            (Customers as RepositoryBase)?.SetTransaction(_transaction);
            (Orders as RepositoryBase)?.SetTransaction(_transaction);
            (OrderItems as RepositoryBase)?.SetTransaction(_transaction);
            (InventoryMovements as RepositoryBase)?.SetTransaction(_transaction);
        }

        public void Commit()
        {
            _transaction?.Commit();
            _transaction = null;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction = null;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection.Dispose();
        }
    }
}
