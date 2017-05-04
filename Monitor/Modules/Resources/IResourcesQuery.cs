using System;
using Monitor.Modules.Resources.Get;

namespace Monitor.Modules.Resources
{
    internal interface IResourcesQuery
    {
        Resource GetById(Guid guid);
        ResourcesResponse Get(ResourcesQueryParameters parameters);
    }
}