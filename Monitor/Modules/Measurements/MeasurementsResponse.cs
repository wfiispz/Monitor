namespace Monitor.Modules.Measurements
{
    public class MeasurementsResponse
    {
        public Sensor[] Measurements { get; set; }
        public PageDetails Page { get; set; }
    }
}