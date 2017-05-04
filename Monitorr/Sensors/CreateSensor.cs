using Monitor.CommandBus;

namespace Monitor.Sensors
{
    public class CreateSensor: ICommand
    {
        public string MeasurementType { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}