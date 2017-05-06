using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication.UpdateSensor
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