
namespace OnionArchitecture.UI.Web.Helpers.Alerts
{
    public class Alert
    {
        public string AlertClass { get; private set; }
        public string Message { get; private set; }

        public Alert(string alertClass, string message)
        {
            AlertClass = alertClass;
            Message = message;
        }
    }
}