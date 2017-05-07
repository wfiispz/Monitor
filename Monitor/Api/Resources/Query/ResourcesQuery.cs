using System;
using System.Linq;
using AutoMapper;
using NHibernate;

namespace Monitor.Api.Resources.Query
{
    // todo tests
    class ResourcesQuery : IResourcesQuery
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IMapper _mapper;

        public ResourcesQuery(ISessionFactory sessionFactory, IMapper mapper)
        {
            _sessionFactory = sessionFactory;
            _mapper = mapper;
        }

        public Resource GetById(Guid guid)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var resource = session.QueryOver<Database.Resource>().Where(x => x.Guid == guid)
                    .JoinQueryOver(x => x.Sensors).SingleOrDefault();
                
                if (resource==null)
                    throw new ArgumentException("resource with given guid does not exist");

                return _mapper.Map<Resource>(resource);
            }
        }

        public ResourcesResponse Get(ResourcesQueryParameters parameters)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var resources = session.QueryOver<Database.Resource>()
                    .Skip(parameters.PageSize * (parameters.Page - 1))
                    .Take(parameters.PageSize)
                    .List();

                var resourcesCount = session.QueryOver<Database.Resource>()
                    .RowCount();

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