using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Driver.Net.Infrastructure.Mapper
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
                {
                    new CustomPropertyTypeMap(
                       typeof(T),
                       (type, columnName) =>
                           type.GetProperties().FirstOrDefault(prop =>
                               prop.GetCustomAttributes(false)
                                   .OfType<ColumnMappingAttribute>()
                                   .Any(attr => attr.DbName.ToUpper() == columnName.ToUpper())
                               )
                       ),
                    new DefaultTypeMap(typeof(T))
                })
        {
        }
    }
}
