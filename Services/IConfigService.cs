using System.Collections.Generic;

namespace Lou.Services {
    public interface IConfigService {
        Dictionary<string, string> GetConfig(string type);
        void SetConfig(string type, Dictionary<string, string> config);
        void SetProperty(string type, string key, string value);
        string GetProperty(string type, string key);
    }
}