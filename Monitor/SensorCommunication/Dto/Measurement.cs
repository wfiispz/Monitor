using System;

namespace Monitor.SensorCommunication.Dto
{
    public class Measurement
    {
        public DataType DataType { get; set; }
        public MeasurementValue[] MesauresArray { get; set; }
        public DateTime Timestamp { get; set; }
    }
}