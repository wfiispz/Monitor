using System;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements
{
    [CommandHandler(typeof(DeleteValues))]
    internal class DeleteValuesHandler : IHandleCommand<DeleteValues>
    {
        private ISessionFactory _sessionFactory;

        public DeleteValuesHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(DeleteValues command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var values = session.QueryOver<Measurement>()
                        .JoinQueryOver(x => x.Sensor)
                        .Where(x => x.Guid == command.Id)
                        .List();

                    foreach (var measurement in values)
                    {
                        session.Delete(measurement);
                    }
                    transaction.Commit();
                    return new object();
                }
            }
        }
    }
}