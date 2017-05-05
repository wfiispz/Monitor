using System;
using Monitor.Modules.Resources.Get;
using Monitor.Persistence;

namespace Monitor.Modules.Resources
{
    class ResourcesQuery : IResourcesQuery
    {
        private readonly IDbAdapter _dbAdapter;

        public ResourcesQuery(IDbAdapter dbAdapter)
        {
            _dbAdapter = dbAdapter;
        }

        public Resource GetById(Guid guid)
        {
            throw new NotImplementedException();
        }

        public ResourcesResponse Get(ResourcesQueryParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}