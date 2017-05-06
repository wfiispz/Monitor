using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication
{
    [CommandHandler(typeof(UpdateSensor))]
    internal class UdateSensorHandler : IHandleCommand<UpdateSensor>
    {
        public object Handle(UpdateSensor command)
        {
            throw new NotImplementedException();
        }
    }
}