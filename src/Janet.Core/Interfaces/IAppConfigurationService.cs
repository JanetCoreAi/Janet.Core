namespace Janet.Core.Interfaces;

public interface IAppConfigurationService<TConfig>
        where TConfig : class, new()
    {
        TConfig Configuration { get; }
        void LoadConfiguration();
        void SaveConfiguration();
    }

    /*
    services.AddSingleton<IAppConfigurationService<MyAppConfiguration>>(provider =>
    new AppConfigurationService<MyAppConfiguration>("path/to/config.json"));
    */