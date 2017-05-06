using System;
using Monitor.Config;
using Monitor.SensorCommunication;
using Nancy.Hosting.Self;

namespace Monitor
{
    public class NancyApiHost
    {
        private NancyHost _host;

        public void Start()
        {
            var configuration = new ConfigurationLoader(new JsonDeserializer()).Load();
            _host = new NancyHost(new Uri(configuration.UrlBasePath));
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
            _host.Dispose();
        }
    }
}
