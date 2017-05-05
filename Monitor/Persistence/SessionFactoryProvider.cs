using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Monitor.Persistence
{
    class SessionFactoryProvider
    {
        public ISessionFactory Create(bool exportSchema)
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("monitor.db"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<SessionFactoryProvider>()).ExposeConfiguration(BuildSchema(exportSchema))
                .BuildSessionFactory();
        }

        private Action<Configuration> BuildSchema(bool exportSchema)
        {
            return config =>
            {
                new SchemaExport(config).Create(true, exportSchema);
            };
        }
    }
}
