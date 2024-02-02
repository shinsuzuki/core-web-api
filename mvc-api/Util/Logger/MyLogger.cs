using NLog;

namespace mvc_api.Util.Logger
{
    public static class MyLogger 
    {
        private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

        static MyLogger()
        {
            LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public static void Trace(string message)
        {
            logger.Trace(message);
        }
    }
}
