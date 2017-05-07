namespace Monitor.Modules.Measurements.Query
{
    public class MeasurementsResponse
    {
        public Sensor[] Measurements { get; set; }
        public PageDetails Page { get; set; }
    }
}