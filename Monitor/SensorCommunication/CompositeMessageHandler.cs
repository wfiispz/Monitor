using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.Logging;
using Newtonsoft.Json.Linq;

namespace Monitor.SensorCommunication
{
    class CompositeMessageHandler : IMessageHandler
    {
        private readonly Dictionary<DataType, ISingleMessageTypeHandler> _handlers;
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly ILogger _logger;

        public CompositeMessageHandler(IEnumerable<ISingleMessageTypeHandler> messageHandlers,
            IJsonDeserializer jsonDeserializer, ILogger logger)
        {
            _jsonDeserializer = jsonDeserializer;
            _logger = logger;
            _handlers = messageHandlers.ToDictionary(x => x.SupportedType, x => x);
        }

        public void Handle(string message)
        {
            try
            {
                _logger.LogInfo($"Handling sensor message: {message}");
                var json = _jsonDeserializer.Deserialize<JObject>(message);
                var dataType = (DataType) Enum.Parse(typeof(DataType), json["datatype"].ToString(), true);
                _handlers[dataType].Handle(message);
            }
            catch (Exception exception)
            {
                _logger.LogError("Error while handling sensor message", exception);
            }
        }
    }
}