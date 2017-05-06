using System;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication
{
    internal class AddMeasurement : ICommand
    {
        public Guid Guid { get; set; }
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }
}