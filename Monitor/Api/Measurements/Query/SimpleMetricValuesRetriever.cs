using System;
using System.Linq;
using AutoMapper;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements.Query
{
    internal class SimpleMetricValuesRetriever : ISimpleMetricValuesRetriever
    {
        private readonly IMapper _mapper;
        private readonly IPathBuilder _pathBuilder;
        private const int MaxMeasurementsLimit = 1000;

        public SimpleMetricValuesRetriever(IMapper mapper, IPathBuilder pathBuilder)
        {
            _mapper = mapper;
            _pathBuilder = pathBuilder;
        }

        public ValuesResponse GetSimpleMetric(ValuesQueryParameters parameters, ISession session)
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