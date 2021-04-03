using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MessageAPI.Repository
{
    public abstract class BaseRepository
    {
        /// <summary>
        /// Базовый репозиторий, включающий в себя методы для подключения к СУБД.
        /// </summary>
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private DbConnection _dataBase;
        /// <summary>
        /// Получает параметры из конфигурационного файла для подключения к СУБД.
        /// </summary>
        /// <param name="configuration"></param>
        protected BaseRepository(IConfiguration configuration,DbConnection database)
        {
            _configuration = configuration;
            _connectionString = configuration["DBInfo:ConnectionString"];
            _dataBase = database;
        }

        /// <summary>
        /// Подключение к СУБД для буферезированных запросов возвращающих тип.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getData"></param>
        /// <returns></returns>
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            try
            {
                await using (var connection = _dataBase)
                {
                    await connection.OpenAsync();
                    return await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }

        /// <summary>
        /// Подключение к СУБД для буферезированных запросов не возвращающих тип.
        /// </summary>
        /// <param name="getData"></param>
        /// <returns></returns>
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            try
            {
                await using (var connection = _dataBase)
                {
                    await connection.OpenAsync();
                    await getData(connection);
                }
            }
            catch (TimeoutException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL timeout", GetType().FullName), ex);
            }
            catch (NpgsqlException ex)
            {
                throw new Exception(String.Format("{0}.WithConnection() experienced a SQL exception (not a timeout)", GetType().FullName), ex);
            }
        }
    }
}
