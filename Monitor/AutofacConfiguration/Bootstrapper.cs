using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var udpHost = container.Resolve<SensorUdpHost>();
            udpHost.Start();
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext ctx)
        {
            var logger = container.Resolve<ILogger>();
            pipelines.OnError.AddItemToStartOfPipeline((context, exception) =>
            {
                logger.LogError("Something is no yes", exception);
                return null;
            });

            pipelines.AfterRequest.AddItemToEndOfPipeline(context =>
            {
                logger.LogInfo($"Processed HTTP request: {Environment.NewLine}{BuildString(context.Request)}{Environment.NewLine}Returning response: {BuildString(context.Response)}");
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