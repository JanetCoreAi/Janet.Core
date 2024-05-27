using Janet.Core.Models.Infrastructure;
using Janet.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Janet.Core.Interfaces
{
    public interface IFileManagerService
    {
        public string CorePath { get; set; }
        public string CoreName { get; set; }
        public string DatabasePath { get; set; }
        public string DatabaseFile { get; set; }
        public string ConfigurationPath { get; set; }
        public string ConfigurationFile { get; set; }
        public string LogsPath { get; set; }
        public string LogsFile { get; set; }

        public void InitializeProgramFiles();
    }
}
