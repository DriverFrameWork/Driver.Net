using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Mapper
{
    public class TableMappingAttribute
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string TableName { get; set; }
    }
}
