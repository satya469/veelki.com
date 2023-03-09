using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Veelki.Data.Infrastructure;

namespace Veelki.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly IDatabase _database;
        IDbTransaction _transaction = null;
        IDbConnection _connection = null;
        private bool _isDisposed;

        public BaseRepository(IDatabase database)
        {
            _database = database;
            _connection = _database.NewConnection();
        }

        //public BaseRepository()
        //{
        //}

        public int Execute(string sql, object param = null) =>
            WithConnection(c => { return c.Execute(sql, param, _transaction); });

        public object ExecuteScalar(string sql, object param = null) =>
            WithConnection(c => { return c.ExecuteScalar(sql, param, _transaction); });

        public TReturn ExecuteScalar<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.ExecuteScalar<TReturn>(sql, param, _transaction); });

        public IEnumerable<TReturn> Query<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.Query<TReturn>(sql, param, _transaction); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id") =>
            WithConnection(c => { return c.Query(sql, map, param, _transaction, splitOn: splitOn); });

        public TReturn QueryFirst<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.QueryFirst<TReturn>(sql, param, _transaction); });

        public TReturn QueryFirstOrDefault<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.QueryFirstOrDefault<TReturn>(sql, param, _transaction); });

        public SqlMapper.GridReader QueryMultiple(string sql, object param = null) =>
            WithConnection(c => { return c.QueryMultiple(sql, param, _transaction); });

        public TReturn QuerySingle<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.QuerySingle<TReturn>(sql, param, _transaction); });

        public TReturn QuerySingleOrDefault<TReturn>(string sql, object param = null) =>
            WithConnection(c => { return c.QuerySingleOrDefault<TReturn>(sql, param, _transaction); });

        #region Async
        public async Task<IEnumerable<TReturn>> ExecuteStoredProcedureAsync<TReturn>(string sql, object param = null, CommandType commandType = default) => await WithConnectionAsync(async c => { return await c.QueryAsync<TReturn>(sql, param, _transaction, commandType: CommandType.StoredProcedure); });

        public async Task<object> ExecuteScalarProcedureAsync(string sql, object param = null, CommandType commandType = default) =>
            await WithConnectionAsync(async c => { return await c.ExecuteScalarAsync(sql, param, _transaction, commandType: CommandType.StoredProcedure); });

        public async Task<int> ExecuteProcedure(string sql, object param = null, CommandType commandType = default) =>
            await WithConnectionAsync(async c => { return await c.ExecuteAsync(sql, param, _transaction, commandType: CommandType.StoredProcedure); });

        public async Task<int> ExecuteAsync(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.ExecuteAsync(sql, param, _transaction); });

        public async Task<object> ExecuteScalarAsync(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.ExecuteScalarAsync(sql, param, _transaction); });

        public async Task<TReturn> ExecuteScalarAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.ExecuteScalarAsync<TReturn>(sql, param, _transaction); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QueryAsync<TReturn>(sql, param, _transaction); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id") =>
            await WithConnectionAsync(async c => { return await c.QueryAsync(sql, map, param, _transaction, splitOn: splitOn); });

        public async Task<TReturn> QueryFirstAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QueryFirstAsync<TReturn>(sql, param, _transaction); });

        public async Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QueryFirstOrDefaultAsync<TReturn>(sql, param, _transaction); });

        public async Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QueryMultipleAsync(sql, param, _transaction); });

        public async Task<List<object>> GetQueryMultipleAsync(string sql, object parameters, params Func<SqlMapper.GridReader, object>[] readerFuncs)
        {
            var returnResults = new List<object>();
            return await WithConnectionAsync(async c =>
            {
                var gridReader = await c.QueryMultipleAsync(sql, parameters, _transaction);
                foreach (var readerFunc in readerFuncs)
                {
                    var obj = readerFunc(gridReader);
                    returnResults.Add(obj);
                }
                return returnResults;
            });
        }

        public async Task<TReturn> QuerySingleAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QuerySingleAsync<TReturn>(sql, param, _transaction); });

        public async Task<TReturn> QuerySingleOrDefaultAsync<TReturn>(string sql, object param = null) =>
            await WithConnectionAsync(async c => { return await c.QuerySingleOrDefaultAsync<TReturn>(sql, param, _transaction); });

        #endregion

        #region Protected methods

        protected TReturn WithConnection<TReturn>(Func<IDbConnection, TReturn> execute)
        {
            //using var connection = _database.NewConnection();
            //connection.Open();
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
            return execute(_connection);
        }

        protected async Task<TReturn> WithConnectionAsync<TReturn>(Func<IDbConnection, Task<TReturn>> execute)
        {
            //_connection = await _database.NewConnectionAsync();
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
            return await execute(_connection);
        }

        //protected TReturn WithSqlConnection<TReturn>(Func<SqlConnection, TReturn> execute)
        //{
        //    using var connection = _database.NewSqlConnection();
        //    //connection.Open();
        //    return execute(connection);
        //}

        public void StartTransaction()
        {
            if (_transaction == null)
                _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        #endregion

        #region Dapper.SimpleCrud

        public async Task<TReturn> GetDataByIdAsync<TReturn>(object param) =>
            await WithConnectionAsync(async c => { return await c.GetAsync<TReturn>(param, _transaction); });

        public async Task<List<TReturn>> GetListPagedAsync<TReturn>(int pageNumber, int rowsCount, string conditions = null, string orderBy = null, object param = null) =>
          (List<TReturn>)await WithConnectionAsync(async c => { return await c.GetListPagedAsync<TReturn>(pageNumber, rowsCount, conditions, orderBy, param, _transaction); });

        public async Task<List<TReturn>> GetListAsync<TReturn>() =>
        (List<TReturn>)await WithConnectionAsync(async c => { return await c.GetListAsync<TReturn>(null, transaction: _transaction); });

        public async Task<int> RecordCountAsync<TReturn>(string conditions = null, object param = null) =>
            await WithConnectionAsync(async c => { return await c.RecordCountAsync<TReturn>(conditions, param, _transaction); });

        public async Task<int?> InsertAsync<TReturn>(TReturn entityToUpdate, IDbTransaction dbTransaction = null)
        {
            return await WithConnectionAsync(async c => { return await c.InsertAsync(entityToUpdate, _transaction); });
            //return await _connection.InsertAsync(entityToUpdate);
        }

        public async Task<int> UpdateAsync<TReturn>(TReturn entityToUpdate)
        {
            return await WithConnectionAsync(async c => { return await c.UpdateAsync(entityToUpdate, _transaction); });
        }

        public async Task<int> DeleteAsync<TReturn>(object id)
        {
            return await WithConnectionAsync(async c => { return await c.DeleteAsync<TReturn>(id, _transaction); });
        }

        public async Task<IEnumerable<T>> SelectAsync<T>(IDictionary<string, object> criteria, IDictionary<string, object> notEqual = null)
        {
            var properties = ParseProperties(criteria);
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", typeof(T).Name, sqlPairs);
            return await WithConnectionAsync(async c => { return await c.QueryAsync<T>(sql, properties.AllPairs, _transaction, commandType: CommandType.Text); });
        }
        public async Task<IEnumerable<T>> SearchSelectAsync<T>(IDictionary<string, object> criteria, IDictionary<string, object> notEqual = null)
        {
            var properties = ParseProperties(criteria);
            var sqlPairs = GetSqlPairs(properties.AllNames, " OR ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", typeof(T).Name, sqlPairs);
            return await WithConnectionAsync(async c => { return await c.QueryAsync<T>(sql, properties.AllPairs, commandType: CommandType.Text); });
        }

        public async Task<T> SelectFirstOrDefaultAsync<T>(IDictionary<string, object> criteria)
        {
            var properties = ParseProperties(criteria);
            var sqlPairs = GetSqlPairs(properties.AllNames, " AND ");
            var sql = string.Format("SELECT * FROM [{0}] WHERE {1}", typeof(T).Name, sqlPairs);
            return await WithConnectionAsync(async c => { return await c.QueryFirstOrDefaultAsync<T>(sql, properties.AllPairs, _transaction, commandType: CommandType.Text); });
        }

        #region Bulk Insert
        public async Task BulkInsert<T>(IEnumerable<T> data, IDbTransaction transaction = null, int batchSize = 0, int bulkCopyTimeout = 30, bool identityInsert = false)
        {
            var type = typeof(T);
            var tableName = TableMapper.GetTableName(type);
            var allProperties = PropertiesCache.TypePropertiesCache(type);
            var keyProperties = PropertiesCache.KeyPropertiesCache(type);
            var computedProperties = PropertiesCache.ComputedPropertiesCache(type);
            var columns = PropertiesCache.GetColumnNamesCache(type);

            var insertProperties = allProperties.Except(computedProperties).ToList();

            if (!identityInsert)
                insertProperties = insertProperties.Except(keyProperties).ToList();

            var (identityInsertOn, identityInsertOff, sqlBulkCopyOptions) = GetIdentityInsertOptions(identityInsert, tableName);

            var insertPropertiesString = GetColumnsStringSqlServer(insertProperties, columns);
            var insertPropertiesValuesString = GetColumnsValuesStringSqlServer(insertProperties, columns);
            await _connection.ExecuteAsync($@"
                {identityInsertOn}
                INSERT INTO {FormatTableName(tableName)}({insertPropertiesString}) values({insertPropertiesValuesString})", data, _transaction);
        }

        public async Task<int> BulkUpdate<T>(IEnumerable<T> data, SqlTransaction transaction = null, int batchSize = 0, int bulkCopyTimeout = 30, bool identityInsert = false)
        {
            var type = typeof(T);
            var tableName = TableMapper.GetTableName(type);
            var allProperties = PropertiesCache.TypePropertiesCache(type);
            var keyProperties = PropertiesCache.KeyPropertiesCache(type);
            var computedProperties = PropertiesCache.ComputedPropertiesCache(type);
            var columns = PropertiesCache.GetColumnNamesCache(type);

            var insertProperties = allProperties.Except(computedProperties).ToList();

            if (!identityInsert)
                insertProperties = insertProperties.Except(keyProperties).ToList();

            var updatePropertiesString = GetUpdateColumnsStringSqlServer(insertProperties, columns);
            string query = $@"Update {FormatTableName(tableName)} set {updatePropertiesString} where {FormatTableName(tableName)}.id = @id";
            return await _connection.ExecuteAsync(query, data, _transaction);
        }

        private static (string identityInsertOn, string identityInsertOff, SqlBulkCopyOptions bulkCopyOptions)
           GetIdentityInsertOptions(bool identityInsert, string tableName)
           => identityInsert
               ? ($"SET IDENTITY_INSERT {FormatTableName(tableName)} ON",
                   $"SET IDENTITY_INSERT {FormatTableName(tableName)} OFF", SqlBulkCopyOptions.KeepIdentity)
               : (string.Empty, string.Empty, SqlBulkCopyOptions.Default);

        internal static string FormatTableName(string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                return table;
            }

            var parts = table.Split('.');

            if (parts.Length == 1)
            {
                return $"[{table}]";
            }

            var tableName = "";
            for (int i = 0; i < parts.Length; i++)
            {
                tableName += $"[{parts[i]}]";
                if (i + 1 < parts.Length)
                {
                    tableName += ".";
                }
            }

            return tableName;
        }

        private static string GetColumnsStringSqlServer(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, string> columnNames, string tablePrefix = null)
        {
            return string.Join(", ", properties.Select(property => $"{tablePrefix}[{columnNames[property.Name]}] "));
        }

        private static string GetColumnsValuesStringSqlServer(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, string> columnNames, string tablePrefix = null)
        {
            return string.Join(", ", properties.Select(property => $"{tablePrefix}@{columnNames[property.Name]} "));
        }

        private static string GetUpdateColumnsStringSqlServer(IEnumerable<PropertyInfo> properties, IReadOnlyDictionary<string, string> columnNames, string tablePrefix = null)
        {
            return string.Join(", ", properties.Select(property => $"{tablePrefix}{columnNames[property.Name]} = {tablePrefix}@{columnNames[property.Name]}"));
        }
        #endregion

        #region Reflection support

        /// <summary>
        /// Retrieves a Dictionary with name and value 
        /// for all object properties matching the given criteria.
        /// </summary>
        private static PropertyContainer ParseProperties(IDictionary<string, object> numberNames)
        {
            var propertyContainer = new PropertyContainer();
            foreach (var item in numberNames)
            {
                propertyContainer.AddValue(item.Key, item.Value);
            }

            return propertyContainer;
        }

        /// <summary>
        /// Create a commaseparated list of value pairs on 
        /// the form: "key1=@value1, key2=@value2, ..."
        /// </summary>
        private static string GetSqlPairs(IEnumerable<string> keys, string separator = ", ")
        {
            var pairs = keys.Select(key => string.Format("{0}=@{0}", key)).ToList();
            return string.Join(separator, pairs);
        }

        #endregion

        #endregion
    }
}