using Monitor.Api.Measurements.Query;
using Monitor.CommandBus;
using Nancy;
using Nancy.ModelBinding;

namespace Monitor.Api.Measurements
{
    public class MeasurementsModule:NancyModule
    {
        private readonly IMeasurementsQuery _measurementsQuery;
        private ICommandBus _commandBus;

        public MeasurementsModule(IMeasurementsQuery measurementsQuery, ICommandBus commandBus):base("/measurements")
        {
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

        }
    }
}
