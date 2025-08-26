using Npgsql;
using System.Data;

namespace InventoryManagement.Settings.Base
{
    public abstract class RepositoryBase
    {
        protected readonly IDbConnection _dbConnection;
        protected IDbTransaction? _dbTransaction;

        protected RepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void SetTransaction(IDbTransaction transaction)
        {
            _dbTransaction = transaction;
        }

        // Métodos auxiliares de transação podem ser mantidos, mas usados somente se o repositório iniciar transações sozinho
        public void BeginTransaction()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            _dbTransaction?.Commit();
            _dbTransaction = null;
        }

        public void Rollback()
        {
            _dbTransaction?.Rollback();
            _dbTransaction = null;
        }
    }
}
