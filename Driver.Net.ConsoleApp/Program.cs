using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Driver.Net.Infrastructure;
using Driver.Net.Infrastructure.Logger;

namespace Driver.Net.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory.GetLogger().Error("111asfasdfasdf");
        }
    }
}
