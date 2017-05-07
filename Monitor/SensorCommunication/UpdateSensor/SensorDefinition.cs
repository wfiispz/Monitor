using System;

namespace Monitor.SensorCommunication.UpdateSensor
{
    public class SensorDefinition
    {
        public string MeasureType { get; set; }
        public string Unit { get; set; }
        public float MaxValue { get; set; }
        public Guid MeasureId { get; set; }
    }
}