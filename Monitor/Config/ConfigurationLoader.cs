using System.IO;
using Monitor.SensorCommunication;

namespace Monitor.Config
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        public const string DefaultConfigFilePath = "./Config/configuration.json";

        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly string _configPath;

        public ConfigurationLoader(IJsonDeserializer jsonDeserializer, string configPath)
        {
            _jsonDeserializer = jsonDeserializer;
            _configPath = configPath;
        }

        public Configuration Load()
        {
            return _jsonDeserializer.Deserialize<Configuration>(File.ReadAllText(_configPath));
        }

    }
}