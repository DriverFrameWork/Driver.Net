using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Configuration
{
    public interface IAppSettingReader
    {
        bool CanApply { get; }
        IEnumerable<string> AllKeys { get; }
        string Query(string key);
    }
}
