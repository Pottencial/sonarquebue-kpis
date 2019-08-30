using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace AzureDevOpsDashBoardTest.Mock
{
    public static class LogFactory
    {
        public static ILogger CreateLogger(LoggerTypes type = LoggerTypes.Null)
        {
            if (type == LoggerTypes.List)
                return new ListLogger();
            else
                return NullLoggerFactory.Instance.CreateLogger("Null Logger");
        }
    }
}
