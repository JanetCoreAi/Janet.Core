using Janet.Core.Services.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastructure
{
    public class DataManagementService
    {
        private FileManagerService fileManagerService;
        public DataManagementService(FileManagerService _fileManagerService) 
        {
             fileManagerService = _fileManagerService;
        }

        public void InitializeDatabase()
        {

        }
    }
}
