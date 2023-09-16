using NLog;

namespace TaskManagerJob.Logger
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        public LoggerManager()
        {

        }
        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message, Exception exception) => logger.Error(message);

        public void LogInformation(string message) => logger.Info(message);

        public void LogWarning(string message) => logger.Warn(message);
    }
}
