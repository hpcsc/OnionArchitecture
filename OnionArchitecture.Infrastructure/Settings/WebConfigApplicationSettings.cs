using System.Configuration;
using OnionArchitecture.Core.Infrastructure.Settings;

namespace OnionArchitecture.Infrastructure.Settings
{
    public class WebConfigApplicationSettings : IApplicationSettings
    {
        public string LoggerName { get { return ConfigurationManager.AppSettings["LoggerName"]; } }
    }
}
