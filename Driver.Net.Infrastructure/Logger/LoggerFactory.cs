using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Threading.Tasks;
using System.IO;
using Driver.Net.Infrastructure.Configuration;
//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Driver.Net.Infrastructure.Logger
{
    public class LoggerFactory
    {
        static LoggerFactory()
        {
            var fileName = AppSettingFinder.Query("logger:FileName");
            var path = AppDomain.CurrentDomain.BaseDirectory + fileName; ;
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));

        }
        private static ILog logger;

        public static ILog GetLogger()
        {
            if (logger == null)
                logger =log4net.LogManager.GetLogger(typeof(LoggerFactory));
            return logger;
        }
    }
}
