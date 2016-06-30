using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Data
{
    /// <summary>
    /// 数据连接事务的Session接口
    /// </summary>
    public interface IDbSession: IDisposable 
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        IDbTransaction BeginTransaction(IsolationLevel isolation = IsolationLevel.ReadCommitted);

        void Commit();

        void Rollback();
    }
}
