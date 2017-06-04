using System;
using Monitor.Api.Auth;
using Monitor.Api.Resources;
using Monitor.Api.Resources.Query;
using Monitor.CommandBus;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Monitor.Api.Modules
{
    public class ResourcesModule:NancyModule
    {
        private readonly ICommandBus _commandBus;
        private readonly IResourcesQuery _resourcesQuery;

        public ResourcesModule(ICommandBus commandBus, IResourcesQuery resourcesQuery):base("/resources")
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AccessRights.Access);

            _commandBus = commandBus;
            _resourcesQuery = resourcesQuery;

            Get["/"] = parameters => HandleGet();
            Get["/{id:guid}"] = parameters => HandleGetById(parameters);
            Delete["/{id:guid}"] = parameters => HandleDelete();
        }

        private object HandleDelete()
        {
            var command = this.Bind<DeleteResource>();
            return _commandBus.Handle(command);
        }

        private Resource HandleGetById(dynamic parameters)
        {
            var guid = (Guid) parameters.Id;
            return _resourcesQuery.GetById(guid);
        }

        private ResourcesResponse HandleGet()
        {
            var parameters = this.Bind<ResourcesQueryParameters>();
            return _resourcesQuery.Get(parameters);
        }
    }
}
