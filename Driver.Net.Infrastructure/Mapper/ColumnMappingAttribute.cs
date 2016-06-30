using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Driver.Net.Infrastructure.Mapper
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnMappingAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
