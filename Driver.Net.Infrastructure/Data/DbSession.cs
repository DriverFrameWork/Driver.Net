using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Data
{
    /// <summary>
    /// 数据库连接事务的Session对象
    /// </summary>
    public class DbSession : IDbSession
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">连接</param>
        public DbSession(IDbConnection conn)
        {
            _connection = conn;
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="conn">连接</param>
        /// <param name="trans">事务</param>
        public DbSession(IDbConnection conn, IDbTransaction trans)
        {
            _connection = conn;
            _transaction = trans;
        }
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public IDbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction Transaction
        {
            get { return _transaction; }
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted)
        {
            _transaction = _connection.BeginTransaction(isolation);
            return _transaction;
        }

        public void Commit()
        {
            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {

            if (_connection.State != ConnectionState.Closed)
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
                _connection.Close();
                _connection = null;
            }
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            _transaction.Rollback();
            _transaction = null;
        }
    }
}
