using System;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.File;

namespace Janet.Core.Services.Infrastructure
{
    public class LoggingService : IDisposable
    {
        private readonly ILogger _logger;
        private readonly IDisposable _loggerDisposable;

        public LoggingService()
        {
            // Configure Serilog to write logs to a file and to the console
            var loggerConfiguration = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.Debug();

            _logger = loggerConfiguration.CreateLogger();
            _loggerDisposable = loggerConfiguration.CreateLogger(); // Capture the disposable instance
        }

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            _logger.Information(message);
        }

        public void LogWarning(string message)
        {
            _logger.Warning(message);
        }

        public void LogError(string message, Exception ex = null)
        {
            _logger.Error(ex, message);
        }

        public void Dispose()
        {
            _loggerDisposable?.Dispose(); // Dispose the disposable instance
        }
    }
}