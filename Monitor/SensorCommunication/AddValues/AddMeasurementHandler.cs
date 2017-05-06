using System;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.SensorCommunication.AddValues
{
    [CommandHandler(typeof(AddMeasurement))]
    internal class AddMeasurementHandler : IHandleCommand<AddMeasurement>
    {
        private readonly ISessionFactory _sessionFactory;

        public AddMeasurementHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(AddMeasurement command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var sensor = GetOrCreate(session, command.Guid);
                    var measurement = new Database.Measurement()
                    {
                        Timestamp = command.Timestamp,
                        Value = command.Value,
                        Sensor = sensor
                    };
                    session.Save(measurement);
                    transaction.Commit();
                    return new object();
                }
            }
        }

        private Sensor GetOrCreate(ISession session, Guid guid)
        {
            var sensor = session.QueryOver<Database.Sensor>().Where(x => x.Guid == guid)
                .SingleOrDefault();
            if (sensor == null)
            {
                sensor = new Sensor
                {
                    Guid = guid
                };
                session.Save(sensor);
            }
            return sensor;
        }
    }
}