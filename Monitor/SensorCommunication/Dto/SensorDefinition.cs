using System;

namespace Monitor.SensorCommunication.Dto
{
    public class SensorDefinition
    {
        public string MeasureType { get; set; }
        public string Unit { get; set; }
        public Guid MeasureId { get; set; }
    }
}