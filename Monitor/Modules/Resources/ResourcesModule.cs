using System;
using Monitor.CommandBus;
using Monitor.Modules.Resources.Create;
using Monitor.Modules.Resources.Get;
using Nancy;
using Nancy.ModelBinding;

namespace Monitor.Modules.Resources
{
    class ResourcesModule:NancyModule
    {
        private readonly ICommandBus _commandBus;
        private readonly IResourcesQuery _resourcesQuery;

        public ResourcesModule(ICommandBus commandBus, IResourcesQuery resourcesQuery):base("/resources")
        {
            _commandBus = commandBus;
            _resourcesQuery = resourcesQuery;

            Get["/"] = parameters => HandleGet();
            Get["/{id:guid}"] = parameters => HandleGetById(parameters);
            Post["/"] = parameters => HandlePost();
            Delete["/{id:guid}"] = parameters => HandleDelete();
        }

        private Response HandleDelete()
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

        private Response HandlePost()
        {
            var command = this.Bind<CreateResource>();
            return _commandBus.Handle(command);
        }
    }
}
