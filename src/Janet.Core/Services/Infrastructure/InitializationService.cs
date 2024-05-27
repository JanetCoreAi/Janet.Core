using Janet.Core.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastruture
{
    public class InitializationService
    {
        private FileManagerService fileManagerService;
        private ConfigurationService configurationService;
        private DataManagementService dataManagementService;

        public InitializationService(FileManagerService _fileManagerService, 
            ConfigurationService _configurationService,
            DataManagementService _dataManagementService)
        {
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
