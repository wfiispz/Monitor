﻿using System;
using Monitor.Api.Resources.Query;
using Monitor.CommandBus;
using Nancy;
using Nancy.ModelBinding;

namespace Monitor.Api.Resources
{
    public class ResourcesModule:NancyModule
    {
        private readonly ICommandBus _commandBus;
        private readonly IResourcesQuery _resourcesQuery;

        public ResourcesModule(ICommandBus commandBus, IResourcesQuery resourcesQuery):base("/resources")
        {
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