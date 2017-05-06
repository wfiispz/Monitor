using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication
{
    [CommandHandler(typeof(AddMeasurement))]
    internal class AddMeasurementHandler : IHandleCommand<AddMeasurement>
    {
        public object Handle(AddMeasurement command)
        {
            throw new NotImplementedException();
        }
    }
}