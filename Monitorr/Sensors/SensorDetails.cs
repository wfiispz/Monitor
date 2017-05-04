using System;

namespace Monitor.Sensors
{
    public class SensorDetails
    {
        public SensorDetails(Guid id, string name, string measurementType, string unit, string description)
        {
            Id = id;
            Name = name;
            MeasurementType = measurementType;
            Unit = unit;
            Description = description;
        }

        public Guid Id { get; }
        public string MeasurementType { get; }
        public string Name { get; }
        public string Unit { get; }
        public string Description { get; }
    }
}