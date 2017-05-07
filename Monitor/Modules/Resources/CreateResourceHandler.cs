using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.Modules.Resources
{
    [CommandHandler(typeof(CreateResource))]
    internal class CreateResourceHandler:IHandleCommand<CreateResource>
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IPathBuilder _pathBuilder;

        public CreateResourceHandler(ISessionFactory sessionFactory, IPathBuilder pathBuilder)
        {
            _sessionFactory = sessionFactory;
            _pathBuilder = pathBuilder;
        }

        public object Handle(CreateResource command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var guid = Guid.NewGuid();
                    var resource = new Resource
                    {
                        Guid = guid,
                        Name = command.Name,
                        Description = command.Description
                    };
                    session.Save(resource);
                    var sensorsGuids = CreateSensors(command.Sensors, session, resource).ToArray();

                    transaction.Commit();

                    return new Query.Resource
                    {
                        Id = guid,
                        Name = command.Name,
                        Description = command.Description,
                        Measurements = sensorsGuids.Select(g => _pathBuilder.CreateForSensor(g)).ToArray()
                    };
                }
            }
        }

        private IEnumerable<Guid> CreateSensors(Sensor[] sensors, ISession session, Resource resource)
        {
            foreach (var sensor in sensors)
            {
                var guid = Guid.NewGuid();
                var entity = new Database.Sensor
                {
                    Guid = guid,
                    Metric = sensor.Metric,
                    Unit = sensor.Unit,
                    Resource = resource
                };
                session.Save(entity);
                yield return guid;
            }
        }
    }
}