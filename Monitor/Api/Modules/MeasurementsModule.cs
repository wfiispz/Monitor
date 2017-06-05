using System;
using Monitor.Api.Auth;
using Monitor.Api.Measurements;
using Monitor.Api.Measurements.Query;
using Monitor.CommandBus;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace Monitor.Api.Modules
{
    public class MeasurementsModule:NancyModule
    {
        private readonly IMeasurementsQuery _measurementsQuery;
        private ICommandBus _commandBus;

        public MeasurementsModule(IMeasurementsQuery measurementsQuery, ICommandBus commandBus):base("/measurements")
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AccessRights.Access);

            _measurementsQuery = measurementsQuery;
            _commandBus = commandBus;
            Get["/"] = _ =>
            {
                var queryParameters = this.Bind<MeasurementsQueryParameters>();
                return _measurementsQuery.All(queryParameters);
            };
            Get["/{id:guid}"] = parameters => _measurementsQuery.GetById(parameters.Id);
            Get["/{id:guid}/values"] = _ =>
            {
                var parameters = this.Bind<ValuesQueryParameters>();
                return _measurementsQuery.GetValues(parameters);
            };
            Delete["/{id:guid}/values"] = _ =>
            {
                var command = this.Bind<DeleteValues>();
                return _commandBus.Handle(command);
            };
            // complex
            Post["/"] = parameters =>
            {
                var command = this.Bind<CreateComplexMeasurement>();
                return _commandBus.Handle(command);
            };
            Delete["/{id:guid}"] = parameters =>
            {
                var command = new DeleteComplexMetric
                {
                    Id = (Guid) parameters.id
                };
                return _commandBus.Handle(command);
            };
        }
    }
}
