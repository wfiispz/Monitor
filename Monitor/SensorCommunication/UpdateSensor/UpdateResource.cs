using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication.UpdateSensor
{
    internal class UpdateResource : ICommand
    {
        public Guid Guid { get; set; }
        public SensorDefinition[] Sensors { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}