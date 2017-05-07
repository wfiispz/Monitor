using System;
using System.Linq;
using AutoMapper;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements.Query
{
    internal class MeasurementsQuery : IMeasurementsQuery
    {
        private const int MaxMeasurementsLimit = 1000;
        private readonly IMapper _mapper;
        private readonly IPathBuilder _pathBuilder;
        private readonly ISessionFactory _sessionFactory;

        public MeasurementsQuery(ISessionFactory sessionFactory, IMapper mapper, IPathBuilder pathBuilder)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
            _pathBuilder = pathBuilder;
        }

        public MeasurementsResponse All(MeasurementsQueryParameters queryParameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var sensors = session.QueryOver<Database.Sensor>()
                    .Skip((queryParameters.Page - 1) * queryParameters.PageSize)
                    .Take(queryParameters.PageSize).List();

                var rowCount = session.QueryOver<Database.Sensor>().RowCount();

                return new MeasurementsResponse
                {
                    Measurements = sensors.Select(x => _mapper.Map<Sensor>(x)).ToArray(),
                    Page = new PageDetails
                    {
                        TotalCount = rowCount,
                        Size = queryParameters.PageSize,
                        Number = queryParameters.Page
                    }
                };
            }
        }

        public Sensor GetById(Guid id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var sensor = session.QueryOver<Database.Sensor>().Where(x => x.Guid == id)
                    .JoinQueryOver(x => x.Resource)
                    .SingleOrDefault();

                if (sensor == null)
                    throw new ArgumentException("measurement with given id not found");

                return _mapper.Map<Sensor>(sensor);
            }
        }

        public ValuesResponse GetValues(ValuesQueryParameters parameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Measurement>()
                    .Where(x => x.Timestamp > parameters.From).And(x => x.Timestamp < parameters.To)
                    .JoinQueryOver(x => x.Sensor)
                    .Where(x => x.Guid == parameters.Id);
                var measurementsRowCount = query.RowCount();

                if (measurementsRowCount > MaxMeasurementsLimit)
                    throw new ArgumentException("Too big date/time range.");

                var measurements = query.List();

                return new ValuesResponse
                {
                    Measurements = _pathBuilder.CreateForSensor(parameters.Id),
                    Values = measurements.Select(x => _mapper.Map<SensorValue>(x)).ToArray()
                };
            }
        }
    }
}