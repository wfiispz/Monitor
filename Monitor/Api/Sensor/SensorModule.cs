using Monitor.Config;
using Monitor.SensorCommunication;
using Nancy;
using Nancy.Extensions;

namespace Monitor.Api.Sensor
{
    public class SensorModule:NancyModule
    {
        private readonly IMessageHandler _messageHandler;

        public SensorModule(IMessageHandler messageHandler, Configuration config):base("/sensorapi")
        {
            _messageHandler = messageHandler;

            if (!config.DebugMode) return;

            Post["/"] = _ =>
            {
                _messageHandler.Handle(this.Request.Body.AsString());
                return null;
            };
        }
    }
}
