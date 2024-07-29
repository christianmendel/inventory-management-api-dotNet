using Npgsql;
using System.Data;

namespace InventoryManagement.Settings.Base
{
    public abstract class RepositoryBase
    {
        protected readonly IDbConnection _dbConnection;

        // Recebe IDbConnection em vez de string connectionString
        protected RepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}
