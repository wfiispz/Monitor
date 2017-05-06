using System;

namespace Monitor.SensorCommunication.Dto
{
    public class Metadata
    {
        public DataType DataType { get; set; }
        public SensorDefinition[] MeasuresArray { get; set; }
        public Guid ResourceId { get; set; }
    }
}
