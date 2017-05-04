using System;
using Monitor.CommandBus;

namespace Monitor.Sensors
{
    public class GetSensorById:IQuery<SensorDetails>
    {
        public Guid Id { get; set; }
        public SensorDetails Query()
        {
            throw new NotImplementedException();
        }
    }
}