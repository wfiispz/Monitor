using Newtonsoft.Json;

namespace Monitor.SensorCommunication
{
    class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}