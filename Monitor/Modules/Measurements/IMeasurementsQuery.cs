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
            throw new NotImplementedException();
        }

        public Sensor GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public ValuesResponse GetValues(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}