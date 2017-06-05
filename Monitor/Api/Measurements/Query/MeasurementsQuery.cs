using System;
using System.Linq;
using AutoMapper;
using Monitor.Database;
using NHibernate;

namespace Monitor.Api.Measurements.Query
{
    internal class MeasurementsQuery : IMeasurementsQuery
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;
        private readonly IComplexMetricValuesRetriever _complexMetricValuesRetriever;
        private readonly SimpleMetricValuesRetriever _simpleMetricValuesRetriever;

        public MeasurementsQuery(ISessionFactory sessionFactory, IMapper mapper, IPathBuilder pathBuilder, IComplexMetricValuesRetriever complexMetricValuesRetriever)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
            _simpleMetricValuesRetriever = new SimpleMetricValuesRetriever(mapper, pathBuilder);
            _complexMetricValuesRetriever = complexMetricValuesRetriever;
        }

        public MeasurementsResponse All(MeasurementsQueryParameters queryParameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var sensorsCount = session.QueryOver<Database.Sensor>().RowCount();
                var rowCount = sensorsCount + session.QueryOver<Database.ComplexMetric>().RowCount();

                var firstResult = (queryParameters.Page - 1) * queryParameters.PageSize;
                var sensors = session.QueryOver<Database.Sensor>()
                    .Skip(firstResult)
                    .Take(queryParameters.PageSize).List();

                var complexMetrics = session.QueryOver<ComplexMetric>()
                    .Skip(Math.Max(firstResult - sensorsCount, 0))
                    .Take(queryParameters.PageSize - sensors.Count)
                    .List();

                return new MeasurementsResponse
                {
                    Measurements = sensors.Select(x => _mapper.Map<Sensor>(x))
                        .Concat(complexMetrics.Select(x => _mapper.Map<Sensor>(x)))
                        .ToArray(),
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


                if (sensor != null)
                    return _mapper.Map<Sensor>(sensor);

                var complexMetric = session.QueryOver<ComplexMetric>()
                    .Where(x => x.Guid == id)
                    .JoinQueryOver(x => x.Sensor)
                    .JoinQueryOver(x => x.Resource)
                    .SingleOrDefault();

                if (complexMetric != null)
                    return _mapper.Map<Sensor>(complexMetric);

                throw new ArgumentException("measurement with given id not found");
            }
        }

        public ValuesResponse GetValues(ValuesQueryParameters parameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                ValuesResponse values;
                return _complexMetricValuesRetriever.TryGet(parameters,out values) ? values : _simpleMetricValuesRetriever.GetSimpleMetric(parameters, session);
            }
        }
    }
}