using Serilog.Events;

namespace DEMOWebApi.Logger
{
    public class ApplicationLogger : ILogger
    {
        readonly Serilog.ILogger _logger;

        public ApplicationLogger(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var paramList = state as IReadOnlyList<KeyValuePair<string, object>>;
            if (paramList == null)
            {
                _logger.Write((LogEventLevel)logLevel, exception, state.ToString());
                return;
            }

            _logger.Write((LogEventLevel)logLevel, exception, paramList.Last().Value.ToString(),
                paramList.SkipLast(1).Select(x => x.Value).ToArray());
        }
    }
}
