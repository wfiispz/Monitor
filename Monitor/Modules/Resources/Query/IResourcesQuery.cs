using System;

namespace Monitor.Modules.Resources.Query
{
    public interface IResourcesQuery
    {
        Resource GetById(Guid guid);
        ResourcesResponse Get(ResourcesQueryParameters parameters);
    }
}