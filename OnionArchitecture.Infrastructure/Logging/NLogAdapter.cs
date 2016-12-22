using NLog;
using OnionArchitecture.Core.Infrastructure.Settings;
using OnionLogging = OnionArchitecture.Core.Infrastructure.Logging;

namespace OnionArchitecture.Infrastructure.Logging
{
    public class NLogAdapter : OnionLogging.ILogger
    {
        private readonly Logger _logger;

        public NLogAdapter(IApplicationSettings applicationSettings)
        {
            _logger = LogManager.GetLogger(applicationSettings.LoggerName);
        }

        public void Log(string message)
        {
            _logger.Log(LogLevel.Info, message);

        }
    }
}
