using System;
using AutoMapper;
using NHibernate;

namespace Monitor.Modules.Resources.Get
{
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
                var resource = session.QueryOver<Persistence.Resource>().Where(x => x.Guid == guid)
                    .JoinQueryOver(x => x.Sensors).SingleOrDefault();

                return _mapper.Map<Resource>(resource);
            }
        }

        public ResourcesResponse Get(ResourcesQueryParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}