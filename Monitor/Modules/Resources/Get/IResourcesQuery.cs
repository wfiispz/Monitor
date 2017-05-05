using System;

namespace Monitor.Modules.Resources.Get
{
    public interface IResourcesQuery
    {
        Resource GetById(Guid guid);
        ResourcesResponse Get(ResourcesQueryParameters parameters);
    }
}