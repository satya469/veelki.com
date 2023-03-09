using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Veelki.Data.Repository
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <returns>The number of rows affected.</returns>
        int Execute(string sql, object param = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <returns>The first cell selected as System.Object.</returns>
        object ExecuteScalar(string sql, object param = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="TReturn">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <returns>The first cell returned, as TReturn.</returns>
        TReturn ExecuteScalar<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        IEnumerable<TReturn> Query<TReturn>(string sql, object param = null);
        /// <summary>
        /// Perform a multi-mapping query with 2 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 5 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 6 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 7 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 4 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 3 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        TReturn QueryFirst<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        TReturn QueryFirstOrDefault<TReturn>(string sql, object param = null);
        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <returns></returns>
        SqlMapper.GridReader QueryMultiple(string sql, object param = null);

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="parameters">The parameters to use for this query.</param>
        /// <param name="readerFuncs">The readerFuncs use for get Table Name</param>
        /// <returns>List</returns>
        Task<List<object>> GetQueryMultipleAsync(string sql, object parameters, params Func<SqlMapper.GridReader, object>[] readerFuncs);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        TReturn QuerySingle<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        TReturn QuerySingleOrDefault<TReturn>(string sql, object param = null);

        /// <summary>
        /// Execute parameterized SQL Procedure.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteProcedure(string sql, object param = null, CommandType commandType = default);

        #region Async

        /// <summary>
        /// Execute parameterized SQL Procedure.
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>The number of rows affected.</returns>
        Task<IEnumerable<TReturn>> ExecuteStoredProcedureAsync<TReturn>(string sql, object param = null, CommandType commandType = default);

        /// <summary>
        ///  Execute parameterized SQL Procedure.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="commandType">Sql command type.</param>
        /// <returns>The number of rows affected.</returns>
        Task<object> ExecuteScalarProcedureAsync(string sql, object param = null, CommandType commandType = default);

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteAsync(string sql, object param = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <returns>The first cell selected as System.Object.
        Task<object> ExecuteScalarAsync(string sql, object param = null);
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="TReturn">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <returns>The first cell returned, as TReturn.</returns>
        Task<TReturn> ExecuteScalarAsync<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, object param = null);
        /// <summary>
        /// Perform a multi-mapping query with 2 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 5 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 6 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 7 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 4 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Perform a multi-mapping query with 3 input types. This returns a single type,
        /// combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id");
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        Task<TReturn> QueryFirstAsync<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        Task<TReturn> QueryFirstOrDefaultAsync<TReturn>(string sql, object param = null);
        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <returns></returns>
        Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        Task<TReturn> QuerySingleAsync<TReturn>(string sql, object param = null);
        /// <summary>
        /// Executes a single-row query, returning the data typed as TReturn.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc)
        /// is queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).</returns>
        Task<TReturn> QuerySingleOrDefaultAsync<TReturn>(string sql, object param = null);

        #endregion

        #region Dapper.SimpleCrud operations

        /// <summary>
        /// Get data by Id (applicable on Identity field)
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="param">The parameters to pass, Identity field.</param>
        /// <returns>Return particular row</returns>
        Task<TReturn> GetDataByIdAsync<TReturn>(object param);

        /// <summary>
        /// Insert data into table
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="entityToInsert">Pass model</param>
        /// <returns>Return inserted Id</returns>
        Task<int?> InsertAsync<TReturn>(TReturn entityToInsert, IDbTransaction dbTransaction = null);

        /// <summary>
        /// Update data in table
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="entityToUpdate">Pass model</param>
        /// <returns>Return inserted Id</returns>
        Task<int> UpdateAsync<TReturn>(TReturn entityToUpdate);

        /// <summary>
        /// Get list of table with pagination.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="pageNumber"></param>
        /// <param name="rowsCount"></param>
        /// <param name="conditions"></param>
        /// <param name="orderBy"></param>
        /// <param name="param"></param>
        /// <returns>List of a particular table with pagination.</returns>
        Task<List<TReturn>> GetListPagedAsync<TReturn>(int pageNumber, int rowsCount, string conditions = null, string orderBy = null, object param = null);

        /// <summary>
        /// Get list of table
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <returns>List of a particular table</returns>
        Task<List<TReturn>> GetListAsync<TReturn>();

        /// <summary>
        /// Get record count of a table.
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="conditions"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<int> RecordCountAsync<TReturn>(string conditions = null, object param = null);

        /// <summary>
        /// Delete row of table
        /// </summary>
        /// <typeparam name="TReturn">The type of result to return.</typeparam>
        /// <param name="id">The parameters to pass.</param>
        /// <returns>Affected rows.</returns>
        Task<int> DeleteAsync<TReturn>(object id);

        /// <summary>
        /// For make query and get data with conditions.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="criteria">Set criteria for providing conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> SelectAsync<T>(IDictionary<string, object> criteria, IDictionary<string, object> notEqual = null);

        /// <summary>
        /// For make query and get data with conditions according search.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="criteria">Set criteria for providing conditions.</param>
        /// <returns></returns>
        Task<IEnumerable<T>> SearchSelectAsync<T>(IDictionary<string, object> criteria, IDictionary<string, object> notEqual = null);

        /// <summary>
        /// For make query and get data with conditions.
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="criteria">Set criteria for providing conditions.</param>
        /// <returns>Get single row</returns>
        Task<T> SelectFirstOrDefaultAsync<T>(IDictionary<string, object> criteria);

        /// <summary>
        /// Insert list of table.
        /// </summary>
        /// <typeparam name="T">Table</typeparam>
        /// <param name="data">List of table</param>
        /// <param name="transaction">Sql transaction</param>
        /// <param name="batchSize"></param>
        /// <param name="bulkCopyTimeout"></param>
        /// <param name="identityInsert"></param>
        /// <returns></returns>
        Task BulkInsert<T>(IEnumerable<T> data, IDbTransaction transaction = null, int batchSize = 0, int bulkCopyTimeout = 30, bool identityInsert = false);

        /// <summary>
        /// Update list of table.
        /// </summary>
        /// <typeparam name="T">Table</typeparam>
        /// <param name="data">List of table</param>
        /// <param name="transaction"></param>
        /// <param name="batchSize"></param>
        /// <param name="bulkCopyTimeout"></param>
        /// <param name="identityInsert"></param>
        /// <returns></returns>
        Task<int> BulkUpdate<T>(IEnumerable<T> data, SqlTransaction transaction = null, int batchSize = 0, int bulkCopyTimeout = 30, bool identityInsert = false);
        #endregion

        void StartTransaction();
        void Commit();
        void Rollback();
    }
}