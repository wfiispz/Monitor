using System;
using Monitor.Config;
using Nancy.Hosting.Self;

namespace Monitor
{
    public class NancyApiHost
    {
        private NancyHost _host;

        public void Start(string configPath)
        {
            var configuration = new ConfigurationLoader(new JsonDeserializer(),configPath).Load();
            _host = new NancyHost(new Uri(configuration.UrlBasePath));
            _host.Start();
            Console.Out.WriteLine($"Listening on API endpoint: {configuration.UrlBasePath}");
        }

        public void Stop()
        {
            _host.Stop();
            _host.Dispose();
        }
    }
}
