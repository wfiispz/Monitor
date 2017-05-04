using System;
using Nancy.Hosting.Self;

namespace Monitor
{
    public class NancyApiHost
    {
        private NancyHost _host;

        public void Start()
        {
            _host = new NancyHost(new Uri("http://localhost:8000"));
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
            _host.Dispose();
        }
    }
}
