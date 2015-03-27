using NLog;
using OnionArchitecture.Core.Infrastructure.Logging;
using OnionArchitecture.Core.Infrastructure.Settings;

namespace OnionArchitecture.Infrastructure.Logging
{
    public class NLogAdapter : ILogger
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
