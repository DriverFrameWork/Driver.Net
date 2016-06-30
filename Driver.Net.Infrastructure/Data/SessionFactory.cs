using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Driver.Net.Infrastructure.Configuration;
using DapperExtensions.Sql;

namespace Driver.Net.Infrastructure.Data
{
    public class SessionFactory
    {
        /// <summary>
        /// 根据Provider类型，创建数据库连接
        /// </summary>
        /// <returns></returns>
        private static IDbConnection CreateConnectionByProvider()
        {
           
            var connectionString = AppSettingFinder.Query("db:ConnectionString");
            String provider = AppSettingFinder.Query("db:Provider");
            IDbConnection conn = null;
            switch (provider)
            {
                case "ORACLE":
                    conn = new OracleConnection(connectionString);
                    DapperExtensions.DapperExtensions.SqlDialect = new OracleDialect();
                    break;
                case "MSSQL":
                    conn = new SqlConnection(connectionString);
                    break;

            }
            //IDbConnection conn = new OracleConnection(connectionString);
            return conn;
        }


        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static IDbConnection OpenConnection()
        {
            IDbConnection conn = CreateConnectionByProvider();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            return conn;
        }

        /// <summary>
        /// 创建数据库连接会话
        /// </summary>
        /// <returns></returns>
        public static IDbSession OpenSession()
        {
            IDbConnection conn = OpenConnection();
            IDbSession session = new DbSession(conn);
            return session;
        }

    }
}
