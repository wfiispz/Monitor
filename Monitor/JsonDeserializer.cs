using System.Globalization;
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

        private static readonly JsonConverter[] _converters =
        {
            new StringEnumConverter(),
            new IsoDateTimeConverter(){DateTimeFormat = "yyyy-MM-dd_hh:mm:ss",DateTimeStyles = DateTimeStyles.AssumeLocal}
        };
    }
}