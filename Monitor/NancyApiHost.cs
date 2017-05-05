using System;
using Nancy.Hosting.Self;

namespace Monitor
{
    public class NancyApiHost
    {
        private NancyHost _host;

        public void Start()
        {
            _host = new NancyHost(new Uri(new Configuration().UrlBasePath));
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
            _host.Dispose();
        }
    }
}
