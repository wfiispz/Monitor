using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Monitor
{
    class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value, _converters);
        }

        private readonly JsonConverter[] _converters =
        {
            new StringEnumConverter()
        };
    }
}