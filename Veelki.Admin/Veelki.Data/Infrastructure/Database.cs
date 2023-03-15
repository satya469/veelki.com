using Microsoft.Extensions.Configuration;
using Veelki.Data.UOW;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Veelki.Data.Infrastructure
{
    public class Database : IDatabase
    {
        private readonly string _connectionString;

        private bool _isDisposed;
        SqlConnection _connection;
        

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection"); 
        }

        public DbConnection NewConnection()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
            return _connection;
        }

        public async Task<DbConnection> NewConnectionAsync()
        {
            _connection = new SqlConnection(_connectionString); // NewConnection();            
            await _connection.OpenAsync();
            return _connection;
        }

        public SqlConnection NewSqlConnection()
        {
            _connection = new SqlConnection(_connectionString); // NewConnection();            
            _connection.Open();
            return _connection;
        }

        public async Task<SqlConnection> NewSqlConnectionAsync()
        {
            _connection = new SqlConnection(_connectionString); // NewConnection();            
            await _connection.OpenAsync();
            return _connection;
        }

        public IUnitOfWork NewUnitOfWork() => new UnitOfWork(false);
        public IUnitOfWork NewAsyncUnitOfWork() => new UnitOfWork(true);       

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_isDisposed)
            {
                return;
            }

            // nothing to dispose

            _isDisposed = true;
        }
    }
}
