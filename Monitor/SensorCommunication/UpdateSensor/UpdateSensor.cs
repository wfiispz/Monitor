using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication.UpdateSensor
{
    internal class UpdateSensor : ICommand
    {
        public Guid ResourceGuid { get; set; }
        public Guid SensorGuid { get; set; }
        public string Metric { get; set; }
        public string Unit { get; set; }
    }
}