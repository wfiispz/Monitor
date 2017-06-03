using System;
using System.Collections.Generic;
using System.Linq;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements.Query
{
    internal class ComplexMetricValuesRetriever : IComplexMetricValuesRetriever
    {
        private readonly IPathBuilder _pathBuilder;
        private readonly ISessionFactory _sessionFactory;
        private readonly ISimpleMetricValuesRetriever _simpleValuesRetriever;

        public ComplexMetricValuesRetriever(ISessionFactory sessionFactory,
            ISimpleMetricValuesRetriever simpleValuesRetriever, IPathBuilder pathBuilder)
        {
            _sessionFactory = sessionFactory;
            _simpleValuesRetriever = simpleValuesRetriever;
            _pathBuilder = pathBuilder;
        }

        public bool TryGet(ValuesQueryParameters parameters, out ValuesResponse values)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var metric = session.QueryOver<ComplexMetric>()
                    .Where(x => x.Guid == parameters.Id)
                    .SingleOrDefault();

                if (metric == null)
                {
                    values = null;
                    return false;
                }

                var simpleValues = _simpleValuesRetriever.GetSimpleMetric(
                    new ValuesQueryParameters
                    {
                        Id = metric.Sensor.Guid,
                        From = parameters.From - TimeSpan.FromMilliseconds(metric.WindowSize),
                        To = parameters.To
                    }, session);

                var complexValues = CountComplexValues(metric, simpleValues,parameters);

                values = new ValuesResponse
                {
                    Measurements = _pathBuilder.CreateForSensor(parameters.Id),
                    Values = complexValues
                };
                return true;
            }
        }

        private SensorValue[] CountComplexValues(ComplexMetric metric, ValuesResponse simpleValues, ValuesQueryParameters parameters)
        {
            var values = new List<SensorValue>();
            var windowSize = TimeSpan.FromMilliseconds(metric.WindowSize);
            // TODO this can be optimized if needed
            for (var valueDate = parameters.From;
                valueDate < parameters.To;
                valueDate += TimeSpan.FromMilliseconds(metric.Frequency))
            {
                var aggregateValues =
                    simpleValues.Values.Where(x => x.Timestamp > valueDate && x.Timestamp <= valueDate - windowSize);
                if(! aggregateValues.Any())
                    continue;

                var newValue = new SensorValue()
                {
                    Value = aggregateValues.Select(x=>x.Value).Average(),
                    Timestamp = valueDate
                };
                values.Add(newValue);
            }
            return values.ToArray();
        }
    }
}