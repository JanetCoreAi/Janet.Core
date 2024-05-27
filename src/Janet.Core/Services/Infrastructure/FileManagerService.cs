using Janet.Core.Models.Infrastructure;
using Janet.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Janet.Core.Services.Infrastruture
{
    public class FileManagerService
    {
        public FileManagerService()
        {
            string programDataPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            CoreName = Constants.CoreName;
            CorePath = Path.Combine(programDataPath, CoreName);

            DatabasePath = Path.Combine(CorePath, "Database");
            DatabaseFile = Path.Combine(DatabasePath, "core.sqlite");

            ConfigurationPath = Path.Combine(CorePath, "Configuration");
            ConfigurationFile = Path.Combine(DatabasePath, "config.json");

            LogsPath = Path.Combine(CorePath, "Logs");
            LogsFile = Path.Combine(LogsPath, "events.log");
        }

        public string CorePath { get; set; }
        public string CoreName { get; set; }
        public string DatabasePath { get; set; }
        public string DatabaseFile { get; set; }
        public string ConfigurationPath { get; set; }
        public string ConfigurationFile { get; set; }
        public string LogsPath { get; set; }
        public string LogsFile { get; set; }

        public void InitializeProgramFiles()
        {
            FileHandler.SafeCreateFolder(CorePath);
            FileHandler.SafeCreateFolder(DatabasePath);
            FileHandler.SafeCreateFolder(ConfigurationPath);
            FileHandler.SafeCreateFolder(LogsPath);

            FileHandler.SafeCreateFile(DatabaseFile);
            FileHandler.SafeCreateFile(ConfigurationFile, JsonSerializer.Serialize(new CoreConfiguration()));
            FileHandler.SafeCreateFile(LogsFile);

        }



    }
}
