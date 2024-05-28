using Janet.Core.Services.Infrastruture;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastructure
{
    public class DataManagementService
    {
        
        private readonly ILogger<FileManagerService> logger;
        private FileManagerService fileManagerService;
        public DataManagementService(
            ILogger<FileManagerService> _logger,
            FileManagerService _fileManagerService) 
        {
             fileManagerService = _fileManagerService;
                logger = _logger;
        }

        public void InitializeDatabase()
        {

        }
    }
}
