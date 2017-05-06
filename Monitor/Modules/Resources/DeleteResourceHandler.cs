using System.Data;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.Modules.Resources
{
    [CommandHandler(typeof(DeleteResource))]
    public class DeleteResourceHandler : IHandleCommand<DeleteResource>
    {
        private readonly ISessionFactory _sessionFactory;

        public DeleteResourceHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(DeleteResource command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction(IsolationLevel.RepeatableRead))
                {
                    var resource = session.QueryOver<Resource>().Where(x => x.Guid == command.Id)
                        .SingleOrDefault();
                    session.Delete(resource);
                    transaction.Commit();

                    return new object();
                }
            }
        }
    }
}