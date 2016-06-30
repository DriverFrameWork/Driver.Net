using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Data
{
    public interface IRepository<TModel> where TModel:BaseModel 
    {
        TModel GetById(dynamic primaryId);

        TModel GetSingle(String sql, dynamic param = null, bool buffer = true);

        IEnumerable<TModel> Query(string sql, dynamic param = null, bool buffered = true);

      

        /// <summary>
        /// 执行SQL语句，返回查询结果
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, bool buffered = false);

        /// <summary>
        /// 执行SQL语句，并返回数值
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="conn"></param>
        /// <returns></returns>
        object ExecuteScalar(IDbConnection conn, IDbCommand cmd);

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
        bool Update(TModel model);

        /// <summary>
        /// 更新单条记录
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns>是否成功</returns>
        bool Update(IDbConnection conn, TModel model, IDbTransaction transaction = null);

        /// <summary>
        /// 插入实体
        /// </summary>
        /// <typeparam name="TModel">类型</typeparam>
        /// <param name="model">实体</param>
        /// <returns>主键自增值</returns>
        dynamic Insert(TModel model);

        /// <summary>
        /// 插入单条记录
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        dynamic Insert(IDbConnection conn, TModel model, IDbTransaction transaction = null);

        bool Delete(String primaryId);

        bool Delete(IDbConnection conn, dynamic primaryId, IDbTransaction transaction = null);

        bool Delete(IDbConnection conn, IPredicate predicate, IDbTransaction transaction = null);

        object ExecuteNonQuery(string sql);
    }
}
