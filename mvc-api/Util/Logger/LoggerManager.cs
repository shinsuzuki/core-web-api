using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace mvc_api.Util.Logger
{
    public class LoggerManager : ILoggerManager
    {

        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public void LogTrace(string message)
        {
            logger.Trace(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogFatal(string message)
        {
            logger.Fatal(message);
        } 
    }

}
