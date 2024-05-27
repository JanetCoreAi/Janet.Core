using Janet.Core.Models.Infrastructure;
using Janet.Core.Services.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastructure
{
    public class ConfigurationService : IConfigurationService
    {
        private FileManagerService fileManagerService;
        public ConfigurationService(FileManagerService _fileManagerService)
        {
            fileManagerService = _fileManagerService;
        }

        public void InitializeConfigurationService()
        {
            LoadConfiguration();
        }

        public CoreConfiguration Configuration { get; set; } = new CoreConfiguration();

        public void LoadConfiguration()
        {
            var json = File.ReadAllText(fileManagerService.ConfigurationFile);
            Configuration = JsonSerializer.Deserialize<CoreConfiguration>(json);
        }

        public void SaveConfiguration() 
        { 
            var json = JsonSerializer.Serialize(Configuration);
            File.WriteAllText(fileManagerService.ConfigurationFile, json);
        }
    }
}
