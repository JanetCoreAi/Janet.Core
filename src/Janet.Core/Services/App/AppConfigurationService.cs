using System.Text.Json;

namespace Janet.Core.Services.App
{
    public class AppConfigurationService<TConfig> : IAppConfigurationService<TConfig>
        where TConfig : class, new()
    {
        private readonly string _configFilePath;

        public AppConfigurationService(string configFilePath)
        {
            _configFilePath = configFilePath;
        }

        public TConfig Configuration { get; private set; }

        public void LoadConfiguration()
        {
            if (File.Exists(_configFilePath))
            {
                string json = File.ReadAllText(_configFilePath);
                Configuration = JsonSerializer.Deserialize<TConfig>(json);
            }
            else
            {
                Configuration = new TConfig();
            }
        }

        public void SaveConfiguration()
        {
            string json = JsonSerializer.Serialize(Configuration, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_configFilePath, json);
        }
    }

    public interface IAppConfigurationService<TConfig>
        where TConfig : class, new()
    {
        TConfig Configuration { get; }
        void LoadConfiguration();
        void SaveConfiguration();
    }
}