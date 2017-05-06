using System;
using System.Linq;
using AutoMapper;
using NHibernate;

namespace Monitor.Modules.Measurements
{
    internal interface IMeasurementsQuery
    {
        MeasurementsResponse All(MeasurementsQueryParameters queryParameters);
        Sensor GetById(Guid id);
        ValuesResponse GetValues(Guid id);
    }

    internal class MeasurementsQuery : IMeasurementsQuery
    {
        private readonly IMapper _mapper;
        private readonly ISessionFactory _sessionFactory;

        public MeasurementsQuery(ISessionFactory sessionFactory, IMapper mapper)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
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

                if(sensor == null)
                    throw new ArgumentException("measurement with given id not found");

                return _mapper.Map<Sensor>(sensor);
            }
        }

        public ValuesResponse GetValues(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}