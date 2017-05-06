using System;

namespace Monitor.Api.Measurements.Query
{
    public class SensorValue
    {
        public float Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}