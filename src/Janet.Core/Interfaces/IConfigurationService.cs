using Janet.Core.Models.Infrastructure;

namespace Janet.Core.Services.Infrastructure
{
    public interface IConfigurationService
    {
        CoreConfiguration Configuration { get; }
        void InitializeConfigurationService();
        void LoadConfiguration();
        void SaveConfiguration();
    }
}