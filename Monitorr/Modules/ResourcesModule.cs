using System;
using Monitor.CommandBus;
using Monitor.Sensors;
using Nancy;
using Nancy.ModelBinding;

namespace Monitor.Modules
{
    /// <summary>
    /// Module to handle sensors operations
    /// </summary>
    public class ResourcesModule:NancyModule
    {
        private readonly Func<dynamic, GetSensors> _getSensorsFactory;
        private readonly ICommandBus _commandBus;
        private Func<dynamic, GetSensorById> _getSensorByIdFactory;

        public ResourcesModule(Func<dynamic, GetSensors> getSensorsFactory, ICommandBus commandBus, Func<dynamic, GetSensorById> getSensorByIdFactory):base("/resources")
        {
            _getSensorsFactory = getSensorsFactory;
            _commandBus = commandBus;
            _getSensorByIdFactory = getSensorByIdFactory;

            Get["/"] = parameters =>
            {
                var getSensors = _getSensorsFactory(parameters);
                return getSensors.Query();
            };
            Get["/{id:guid}"] = parameters =>
            {
                var getSensorById = _getSensorByIdFactory(parameters);
                return getSensorById.Query();
            };

            Post["/"] = parameters =>
            {
                var command = this.Bind<CreateSensor>();
                return _commandBus.Handle(command);
            };

            Delete["/{id:guid}"] = parameters =>
            {
                var command = this.Bind<DeleteSensor>();
                return _commandBus.Handle(command);
            };
        }
    }
}