using System;
using System.Linq;
using AutoMapper;
using Monitor.Database;
using NHibernate;
using NHibernate.Criterion;

namespace Monitor.Api.Resources.Query
{
    // todo tests
    class ResourcesQuery : IResourcesQuery
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;
        private readonly IPathBuilder _pathBuilder;

        public ResourcesQuery(ISessionFactory sessionFactory, IMapper mapper, IPathBuilder pathBuilder)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
            _pathBuilder = pathBuilder;
        }

        public Resource GetById(Guid guid)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var resource = session.QueryOver<Database.Resource>().Where(x => x.Guid == guid)
                    .JoinQueryOver(x => x.Sensors)
                    .SingleOrDefault();

                var complexMetrics = session.QueryOver<ComplexMetric>()
                    .JoinQueryOver(x => x.Sensor)
                    .JoinQueryOver(x => x.Resource)
                    .Where(x => x.Guid == guid).List()
                    .Select(x=>x.Guid);

                if (resource==null)
                    throw new ArgumentException("resource with given guid does not exist");

                var mappedResource = _mapper.Map<Resource>(resource);

                mappedResource.Measurements = mappedResource.Measurements
                    .Concat(complexMetrics.Select(x => _pathBuilder.CreateForSensor(x))).ToArray();

                return mappedResource;
            }
        }

        public ResourcesResponse Get(ResourcesQueryParameters parameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var query = session.QueryOver<Database.Resource>();
                if (!string.IsNullOrWhiteSpace(parameters.Name))
                    query = query.Where(Expression.Like("Name", $"%{parameters.Name}%"));

                var resourcesCount = query.RowCount();

                var resources = query.Skip(parameters.PageSize * (parameters.Page - 1))
                    .Take(parameters.PageSize)
                    .List();

                return new ResourcesResponse
                {
                    Resources = resources.Select(x => _mapper.Map<Resource>(x)).ToArray(),
                    Page = new PageDetails
                    {
                        Number = parameters.Page,
                        Size = parameters.PageSize,
                        TotalCount = resourcesCount
                    }
                };
            }
        }
    }
}