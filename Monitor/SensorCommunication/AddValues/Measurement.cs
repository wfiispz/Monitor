using System;

namespace Monitor.SensorCommunication.AddValues
{
    public class Measurement
    {
        public DataType DataType { get; set; }
        public MeasurementValue[] MeasuresArray { get; set; }
        public DateTime Timestamp { get; set; }
    }
}