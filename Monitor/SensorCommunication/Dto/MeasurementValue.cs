using System;

namespace Monitor.SensorCommunication.Dto
{
    public class MeasurementValue
    {
        public Guid MeasureId { get; set; }
        public float Value { get; set; }
    }
}