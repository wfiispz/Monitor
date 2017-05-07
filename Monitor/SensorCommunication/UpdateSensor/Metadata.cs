using System;

namespace Monitor.SensorCommunication.UpdateSensor
{
    public class Metadata
    {
        public DataType DataType { get; set; }
        public SensorDefinition[] MeasuresArray { get; set; }
        public Guid ResourceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
