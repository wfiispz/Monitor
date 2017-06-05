using System;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using Monitor.Config;
using Monitor.Logging;
using Monitor.SensorCommunication.UdpHost;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Nancy.Extensions;

namespace Monitor.AutofacConfiguration
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private readonly ComponentsRegistrar _componentsRegistrar = new ComponentsRegistrar();

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => _componentsRegistrar.RegisterTypes(builder));
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            StaticConfiguration.DisableErrorTraces = false;
            var config = container.Resolve<Configuration>();

            EnableAuthentication(container, pipelines);

            if (config.LogFullHttp)
                EnableLogging(container, pipelines);

            var udpHost = container.Resolve<SensorUdpHost>();
            udpHost.Start();
        }

        private static void EnableAuthentication(ILifetimeScope container, IPipelines pipelines)
        {
            pipelines.EnableBasicAuthentication(
                new BasicAuthenticationConfiguration(container.Resolve<IUserValidator>(), "Realm"));
        }

        private void EnableLogging(ILifetimeScope container, IPipelines pipelines)
        {
            var logger = container.Resolve<ILogger>();

            pipelines.OnError.AddItemToStartOfPipeline((context, exception) =>
            {
                logger.LogError("Something is no yes", exception);
                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline(context =>
            {
                logger.LogInfo(
                    $"Processed HTTP request: {Environment.NewLine}{BuildString(context.Request)}{Environment.NewLine}Returning response: {BuildString(context.Response)}");
            });
        }

        private string BuildString(Response response)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"StatusCode: {response.StatusCode}");
            builder.AppendLine("Headers:");
            foreach (var header in response.Headers)
            {
                builder.AppendLine($"    {header.Key}:{header.Value}");
            }
            var buffer = new MemoryStream();
            response.Contents.Invoke(buffer);
            builder.AppendLine($"Content-Type:{response.ContentType}, Body:");
            builder.AppendLine(Encoding.ASCII.GetString(buffer.ToArray()));
            return builder.ToString();
        }

        private string BuildString(Request request)
        {
            var builder = new StringBuilder();

            builder.AppendFormat("{0} {1}{2}", request.Method, request.Path, Environment.NewLine);
            builder.AppendFormat("Headers:{0}", Environment.NewLine);
            foreach (var header in request.Headers)
            {
                builder.AppendFormat("    {0}: {1}{2}", header.Key, header.Value.Aggregate((x, y) => x + " " + y),
                    Environment.NewLine);
            }
            request.Body.Position = 0;
            builder.AppendLine($"Body: {request.Body.AsString()}");
            return builder.ToString();
        }
    }
}