using System.Collections.Generic;
using Zenject;

namespace Lou.Services {
    public class ConfigService : IConfigService {
        
        private Dictionary<string, Dictionary<string, string>> _configs;
        
        public ConfigService() {
            _configs = new Dictionary<string, Dictionary<string, string>>();
        }

        public Dictionary<string, string> GetConfig(string type) {
            return _configs[type];
        }

        public void SetConfig(string type, Dictionary<string, string> config) {
            _configs[type] = config;
        }

        public void SetProperty(string type, string key, string value) {
            var config = _configs[type];
            if (config != null) {
                config[key] = value;
            }
        }

        public string GetProperty(string type, string key) {
            if (_configs.TryGetValue(type, out var config)) {
                return config.TryGetValue(key, out string value) ? value : null;
            }

            return null;
        }
    }
}