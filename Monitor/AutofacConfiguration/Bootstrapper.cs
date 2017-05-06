using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Monitor.Api.Index;
using Monitor.Api.Sensor;
using Monitor.CommandBus;
using Monitor.Config;
using Monitor.Database;
using Monitor.Mapping;
using Nancy;
using Nancy.Bootstrappers.Autofac;
using NHibernate;

namespace Monitor.AutofacConfiguration
{
    public class Bootstrapper:AutofacNancyBootstrapper
    {
        private static readonly IList<Func<Type,bool>> RegistrationExcludePredicates = new List<Func<Type, bool>>()
        {
            IsAbstract,
            IsNancyModule,
            IsBootstrapper
        };

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            container.Update(builder => RegisterAssemblyTypes(builder,typeof(IndexModule).Assembly));
        }

        private void RegisterAssemblyTypes(ContainerBuilder builder, Assembly assembly)
        {
            foreach (var type in assembly.GetTypes().Where(type=>RegistrationExcludePredicates.Any(x => x(type))==false))
            {
                builder.RegisterType(type)
                    .AsImplementedInterfaces()
                    .AsSelf();
            }
            var commandsToHandlers = AssignCommandsToHandlers(assembly);
            RegisterCommandHandlers(builder,commandsToHandlers);
            AddCustomRegistrations(builder);
        }

        private void AddCustomRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<SimpleBus>()
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();

            builder.Register(x => x.Resolve<SessionFactoryProvider>()
                    .Create(false, x.Resolve<Configuration>().DatabaseFilepath))
                    .As<ISessionFactory>()
                .SingleInstance().AutoActivate();

            builder.Register(x => x.Resolve<AutomapperProvider>().Create())
                .AsImplementedInterfaces();

            builder.RegisterType<PathBuilder>().WithParameter(new ResolvedParameter(
                (info, _) => info.Name == "urlBasePath",
                (_, context) => context.Resolve<Configuration>().UrlBasePath))
                .AsSelf()
                .AsImplementedInterfaces();


            builder.Register(x => x.Resolve<IConfigurationLoader>().Load())
                .AsSelf()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterCommandHandlers(ContainerBuilder builder, IDictionary<Type, Type> commandsToHandlers)
        {
            foreach (var commandToHandler in commandsToHandlers)
            {
                builder.RegisterType(commandToHandler.Value)
                    .AsSelf()
                    .AsImplementedInterfaces()
                    .Keyed<IHandleCommand>(commandToHandler.Key);
            }
        }

        private IDictionary<Type,Type> AssignCommandsToHandlers(Assembly assembly)
        {
            var commandHandlers =
                assembly.GetTypes().Where(type => type.IsAbstract == false && type.IsAssignableTo<IHandleCommand>());
            var commandsToHandlers = new Dictionary<Type,Type>();
            foreach (var commandHandler in commandHandlers)
            {
                commandsToHandlers.Add(commandHandler.GetCustomAttribute<CommandHandlerAttribute>().CommandType,
                    commandHandler);
            }
            return commandsToHandlers;
        }

        private static bool IsBootstrapper(Type arg)
        {
            return arg == typeof(Bootstrapper);
        }

        private static bool IsNancyModule(Type arg)
        {
            return arg.IsAssignableTo<NancyModule>();
        }

        private static bool IsAbstract(Type arg)
        {
            return arg.IsAbstract;
        }
    }
}