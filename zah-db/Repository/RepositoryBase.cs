using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;

namespace zah_db.Repository
{
    public class RepositoryBase
    {
        private readonly IDbConnection _dbConnection;

        public RepositoryBase(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        /// <summary>
        /// 查询第一条
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<T> QueryFirst<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<IEnumerable<T>> Query<T>(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<T>(sql, parameters);
            }
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected async Task<int> Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, parameters);
            }
        }
        
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        private IDbConnection CreateConnection()
        {
            var connection = _dbConnection;
            _dbConnection.Open();
            return connection;
        }
    }
}
