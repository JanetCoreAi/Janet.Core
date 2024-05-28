using Janet.Core.Interfaces;
using Janet.Core.Services.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastruture
{
    public class InitializationService : IInitializationService
    {
        private FileManagerService fileManagerService;
        private ConfigurationService configurationService;
        private DataManagementService dataManagementService;

        private ILogger<InitializationService> logger;

        public InitializationService(FileManagerService _fileManagerService, 
            ConfigurationService _configurationService,
            DataManagementService _dataManagementService,
            ILogger<InitializationService> _logger)
        {
            logger = _logger;
            fileManagerService = _fileManagerService;
            configurationService = _configurationService;
            dataManagementService = _dataManagementService;
        }

        public void Initialize()
        {
            fileManagerService.InitializeProgramFiles();
            configurationService.InitializeConfigurationService();
            dataManagementService.InitializeDatabase();
            
        }
    }
}
