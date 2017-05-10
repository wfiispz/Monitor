using System;
using System.Linq;
using System.Text;
using Autofac;
using Monitor.Logging;
using Monitor.SensorCommunication.UdpHost;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Extensions;

namespace Monitor.AutofacConfiguration
{
    public class Bootstrapper:AutofacNancyBootstrapper
    {
        private readonly ComponentsRegistrar _componentsRegistrar = new ComponentsRegistrar();

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => _componentsRegistrar.RegisterTypes(builder));
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            StaticConfiguration.DisableErrorTraces = false;
            var logger = container.Resolve<ILogger>();
            pipelines.OnError += (context, exception) =>
            {
                logger.LogError("Something is no yes", exception);
                return null; 
            };

            var udpHost = container.Resolve<SensorUdpHost>();
            udpHost.Start();
        }
    }
}