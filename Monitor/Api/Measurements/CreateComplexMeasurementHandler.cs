using System;
using Monitor.CommandBus;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements
{
    [CommandHandler(typeof(CreateComplexMeasurement))]
    public class CreateComplexMeasurementHandler : IHandleCommand<CreateComplexMeasurement>
    {
        private readonly ISessionFactory _sessionFactory;

        public CreateComplexMeasurementHandler(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public object Handle(CreateComplexMeasurement command)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var baseMeasurement = session.QueryOver<Database.Sensor>()
                    .Where(x => x.Guid == command.BaseMeasurement).SingleOrDefault();

                if (baseMeasurement == null)
                    throw new ArgumentException($"No sensor with id {command.BaseMeasurement} found");

                using (var transaction = session.BeginTransaction())
                {
                    var newComplex = new ComplexMetric()
                    {
                        Guid = Guid.NewGuid(),
                        Frequency = command.Frequency,
                        Sensor = baseMeasurement,
                        TimeStart = DateTime.Now,
                        WindowSize = command.Windowsize,
                        Description = command.Description
                    };
                    session.Save(newComplex);
                    transaction.Commit();
                    return new object();
                }
            }
        }
    }
}