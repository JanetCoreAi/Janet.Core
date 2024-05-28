using Janet.Core.Interfaces;
using Janet.Core.Models.Infrastructure;
using Janet.Core.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

namespace Janet.Core.Services.Infrastructure
{
    public class FileManagerService : IFileManagerService
    {
        private readonly ILogger<FileManagerService> _logger;

        public FileManagerService(ILogger<FileManagerService> logger)
        {
            _logger = logger;
            string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            CoreName = Constants.CoreName;
            CorePath = Path.Combine(programDataPath, CoreName);
            DatabasePath = Path.Combine(CorePath, "Database");
            DatabaseFile = Path.Combine(DatabasePath, "core.sqlite");
            ConfigurationPath = Path.Combine(CorePath, "Configuration");
            ConfigurationFile = Path.Combine(ConfigurationPath, "config.json");
            LogsPath = Path.Combine(CorePath, "Logs");
            LogsFile = Path.Combine(LogsPath, "events.log");
        }

        public string CorePath { get; }
        public string CoreName { get; }
        public string DatabasePath { get; }
        public string DatabaseFile { get; }
        public string ConfigurationPath { get; }
        public string ConfigurationFile { get; }
        public string LogsPath { get; }
        public string LogsFile { get; }

        public void InitializeProgramFiles()
        {
            try
            {
                FileHandler.SafeCreateFolder(CorePath);
                FileHandler.SafeCreateFolder(DatabasePath);
                FileHandler.SafeCreateFolder(ConfigurationPath);
                FileHandler.SafeCreateFolder(LogsPath);
                FileHandler.SafeCreateFile(DatabaseFile);
                FileHandler.SafeCreateFile(ConfigurationFile, JsonSerializer.Serialize(new CoreConfiguration(), new JsonSerializerOptions { WriteIndented = true }));
                FileHandler.SafeCreateFile(LogsFile);
                _logger.LogInformation("Program files initialized successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error initializing program files.");
            }
        }
    }
}