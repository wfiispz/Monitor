using System;
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
            var logger = container.Resolve<ILogger>();
            pipelines.BeforeRequest += (context, cancellationToken) =>
            {

                logger.LogInfo($"Processing HTTP request: {Environment.NewLine}{BuildString(context.Request)}");
                return null;
            };

            pipelines.AfterRequest += context =>
            {
                logger.LogInfo($"Returning response: {context.Response.StatusCode}");
            };

            var udpHost = container.Resolve<SensorUdpHost>();
            udpHost.Start();
        }

        private string BuildString(Request request)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} {1}{2}", request.Method, request.Path, Environment.NewLine);
            builder.AppendFormat("Headers:{0}", Environment.NewLine);
            foreach (var header in request.Headers)
            {
                builder.AppendFormat("{0}: {1}{2}", header.Key, header.Value, Environment.NewLine);
            }
            builder.AppendLine(request.Body.AsString());
            return builder.ToString();
        }
    }
}