using Janet.Core.Models.Infrastructure;
using Janet.Core.Services.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;

namespace Janet.Core.Services.Infrastructure
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly FileManagerService _fileManagerService;
        private readonly ILogger<ConfigurationService> _logger;
        private readonly string _configurationFilePath;

        public ConfigurationService(FileManagerService fileManagerService, ILogger<ConfigurationService> logger)
        {
            _fileManagerService = fileManagerService;
            _logger = logger;
            _configurationFilePath = _fileManagerService.ConfigurationFile;
        }

        public CoreConfiguration Configuration { get; private set; } = new CoreConfiguration();

        public void InitializeConfigurationService()
        {
            LoadConfiguration();
        }

        public void LoadConfiguration()
        {
            try
            {
                if (File.Exists(_configurationFilePath))
                {
                    var json = File.ReadAllText(_configurationFilePath);
                    Configuration = JsonSerializer.Deserialize<CoreConfiguration>(json);
                    _logger.LogInformation("Configuration loaded successfully.");
                }
                else
                {
                    _logger.LogWarning("Configuration file not found. Creating a new configuration file.");
                    Configuration = new CoreConfiguration();
                    SaveConfiguration();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading configuration.");
            }
        }

        public void SaveConfiguration()
        {
            try
            {
                var json = JsonSerializer.Serialize(Configuration, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_configurationFilePath, json);
                _logger.LogInformation("Configuration saved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving configuration.");
            }
        }
    }
}