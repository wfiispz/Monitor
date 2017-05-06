using System;

namespace Monitor.Api.Resources.Query
{
    public interface IResourcesQuery
    {
        Resource GetById(Guid guid);
        ResourcesResponse Get(ResourcesQueryParameters parameters);
    }
}