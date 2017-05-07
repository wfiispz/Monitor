using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.SensorCommunication.UpdateSensor
{
    [CommandHandler(typeof(UpdateResource))]
    internal class UpdateResourceHandler : IHandleCommand<UpdateResource>
    {
        private readonly ISessionFactory _sessionFactory;

        public UpdateResourceHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(UpdateResource command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var resource = CreateOrUpdateResource(command, session);
                    foreach (SensorDefinition sensor in command.Sensors)
                    {
                        CreateOrUpdateSensor(session, sensor, resource);
                    }
                    transaction.Commit();
                    return new object();
                }
            }
        }

        private void CreateOrUpdateSensor(ISession session, SensorDefinition sensorDefinition, Resource resource)
        {
            var sensor = session.QueryOver<Sensor>()
                .Where(x => sensorDefinition.MeasureId == x.Guid).SingleOrDefault();

            if (sensor == null)
            {
                sensor = new Sensor
                {
                    Guid = sensorDefinition.MeasureId,
                };
            }

            sensor.Resource = resource;
            sensor.Metric = sensorDefinition.MeasureType;
            sensor.Unit = sensorDefinition.Unit;
            session.SaveOrUpdate(sensor);
        }

        private static Resource CreateOrUpdateResource(UpdateResource updateResource, ISession session)
        {
            var resource = session.QueryOver<Database.Resource>()
                .Where(x => x.Guid == updateResource.Guid).SingleOrDefault();
            if(resource == null)
                resource = new Resource
                {
                    Guid = updateResource.Guid,
                };

            resource.Name = updateResource.Name;
            resource.Description = updateResource.Description;

            session.SaveOrUpdate(resource);
            return resource;
        }
    }
}