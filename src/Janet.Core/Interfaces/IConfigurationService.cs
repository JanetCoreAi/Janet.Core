using Janet.Core.Models.Infrastructure;
using Janet.Core.Services.Infrastruture;
using System.Text.Json;

public interface IConfigurationService
{
    public void InitializeConfigurationService();
    public CoreConfiguration Configuration { get; set; }
    public void LoadConfiguration();
    public void SaveConfiguration();
}