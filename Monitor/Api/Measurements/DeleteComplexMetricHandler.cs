using System;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements
{
    [CommandHandler(typeof(DeleteComplexMetric))]
    public class DeleteComplexMetricHandler : IHandleCommand<DeleteComplexMetric>
    {
        private readonly ISessionFactory _sessionFactory;

        public DeleteComplexMetricHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(DeleteComplexMetric command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var metric = session.QueryOver<ComplexMetric>()
                        .Where(x => x.Guid == command.Id).SingleOrDefault();

                    if (metric == null)
                        throw new ArgumentException("No complex metric with given id found");

                    session.Delete(metric);
                    transaction.Commit();
                    return new object();
                }
            }
        }
    }
}