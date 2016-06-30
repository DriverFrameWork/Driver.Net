using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions;
using Dapper;
using System.Linq.Expressions;
using Driver.Net.Infrastructure.Mapper;

namespace Driver.Net.Infrastructure.Data
{
    public class Repository<TModel> : IRepository<TModel> where TModel : BaseModel
    {
        public TModel GetById(dynamic primaryId)
        {
            IDbConnection conn = SessionFactory.OpenConnection();
            try
            {
                return conn.Get<TModel>(primaryId as object);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public TModel GetSingle(string sql, dynamic param = null, bool buffer = true)
        {
            TModel model = null;
            IDbConnection conn = SessionFactory.OpenConnection();
            try
            {
                var list = SqlMapper.Query<TModel>(conn, sql, param as object, null, buffer).ToList();
                if (list != null && list.Count() > 0)
                {
                    model = list[0];
                }
                return model;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public IEnumerable<TModel> Query(string sql, dynamic param = null, bool buffered = true)
        {
            Dapper.SqlMapper.SetTypeMap(typeof(TModel), new ColumnAttributeTypeMapper<TModel>());
            IDbConnection conn = SessionFactory.OpenConnection();
            try
            { 
                return SqlMapper.Query<TModel>(conn, sql, param as object, null, buffered);
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public object ExecuteScalar(IDbConnection conn, IDbCommand cmd)
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public object ExecuteScalar(string sql, bool buffered = false)
        {
            IDbConnection conn = SessionFactory.OpenConnection();
            var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            return ExecuteScalar(conn, cmd);
        }

        public dynamic Insert(TModel model)
        {
            IDbConnection conn = SessionFactory.OpenConnection();
            return conn.Insert(model);
        }

        public dynamic Insert(IDbConnection conn, TModel model, IDbTransaction transaction = null)
        {
            dynamic result = conn.Insert(model, transaction);
            return result;
        }

        public bool Update(TModel model)
        {
            var session = SessionFactory.OpenSession();
            session.BeginTransaction();
            try
            {
                var isOk = Update(session.Connection, model, session.Transaction);
                session.Commit();
                return isOk;
            }
            catch (Exception ex)
            {
                session.Rollback();
                throw ex;
            }
            finally
            {
                session.Dispose();
            }
        }

        public bool Update(IDbConnection conn, TModel model, IDbTransaction transaction = null)
        {
            bool isOk = conn.Update(model, transaction);
            return isOk;
        }

        public bool Delete(string primaryId)
        {
            var session = SessionFactory.OpenSession();
            session.BeginTransaction();
            try
            {
                var isOk = Delete(session.Connection, primaryId, session.Transaction);
                session.Commit();
                return isOk;
            }
            catch (System.Exception)
            {
                session.Rollback();
                throw;
            }
            finally
            {
                session.Dispose();
            }
        }

        public bool Delete(IDbConnection conn, IPredicate predicate, IDbTransaction transaction = null)
        {
            return conn.Delete(predicate, transaction);
        }

        public bool Delete(IDbConnection conn, dynamic primaryId, IDbTransaction transaction = null)
        {
            var entity = GetById(primaryId);
            var obj = entity as TModel;
            bool isOk = conn.Delete(obj, transaction);
            return isOk;
        }

        public object ExecuteNonQuery(string sql)
        {
            IDbConnection conn = SessionFactory.OpenConnection();
            IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            return cmd.ExecuteNonQuery();
        }

      
    }
}
