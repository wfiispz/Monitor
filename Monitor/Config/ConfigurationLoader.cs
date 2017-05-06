using System.IO;
using Monitor.SensorCommunication;

namespace Monitor.Config
{
    internal class ConfigurationLoader : IConfigurationLoader
    {
        private readonly IJsonDeserializer _jsonDeserializer;

        public ConfigurationLoader(IJsonDeserializer jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
        }

        public Configuration Load()
        {
            return _jsonDeserializer.Deserialize<Configuration>(File.ReadAllText(ConfigFilePath));
        }

        private const string ConfigFilePath = "Config/configuration.json";
    }
}