using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Monitor.Modules;
using Nancy;
using Nancy.Bootstrappers.Autofac;

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