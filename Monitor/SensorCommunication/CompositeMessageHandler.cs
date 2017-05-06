using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.SensorCommunication.Dto;
using Newtonsoft.Json.Linq;

namespace Monitor.SensorCommunication
{
    class CompositeMessageHandler : IMessageHandler
    {
        private Dictionary<DataType, ISingleMessageTypeHandler> _handlers;
        private readonly IJsonDeserializer _jsonDeserializer;

        public CompositeMessageHandler(IEnumerable<ISingleMessageTypeHandler> messageHandlers,
            IJsonDeserializer jsonDeserializer)
        {
            _jsonDeserializer = jsonDeserializer;
            _handlers = messageHandlers.ToDictionary(x => x.SupportedType, x => x);
        }

        public void Handle(string message)
        {
            var json = _jsonDeserializer.Deserialize<JObject>(message);
            var dataType = (DataType) Enum.Parse(typeof(DataType), json["dataType"].ToString());
            _handlers[dataType].Handle(message);
        }
    }
}