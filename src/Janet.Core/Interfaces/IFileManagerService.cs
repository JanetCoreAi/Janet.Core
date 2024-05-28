namespace Janet.Core.Interfaces
{
    public interface IFileManagerService
    {
        string CorePath { get; }
        string CoreName { get; }
        string DatabasePath { get; }
        string DatabaseFile { get; }
        string ConfigurationPath { get; }
        string ConfigurationFile { get; }
        string LogsPath { get; }
        string LogsFile { get; }

        void InitializeProgramFiles();
    }
}