    public interface IKeyStorageService
    {
        bool SaveKey(string name, string key);
        string RetrieveKey(string name);
    }