using Monitor.Config;
using Monitor.SensorCommunication;
using Monitor.SensorCommunication.UdpHost;
using Nancy;
using Nancy.Extensions;

namespace Monitor.Api.Sensor
{
    public class SensorModule:NancyModule
    {
        private readonly IMessageHandler _messageHandler;
        // workaround for Autofac Nancy bootstrapper to activate this host
        private readonly SensorUdpHost _sensorUdpHost;

        public SensorModule(IMessageHandler messageHandler, Configuration config, SensorUdpHost sensorUdpHost):base("/sensorapi")
        {
            _messageHandler = messageHandler;
            _sensorUdpHost = sensorUdpHost;

            if (!config.DebugMode) return;

            Post["/"] = _ =>
            {
                _messageHandler.Handle(this.Request.Body.AsString());
                return null;
            };
        }
    }
}
