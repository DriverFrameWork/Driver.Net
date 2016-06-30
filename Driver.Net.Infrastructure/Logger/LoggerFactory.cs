using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Threading.Tasks;

namespace Driver.Net.Infrastructure.Logger
{
    public class LoggerFactory
    {
        private static ILog logger;
        public static ILog GetLogger()
        {
            if (logger == null)
                logger = Logger.LoggerFactory.GetLogger();
            return logger;
        }
    }
}
