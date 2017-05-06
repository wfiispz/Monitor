namespace Monitor.Api.Measurements.Query
{
    public class MeasurementsResponse
    {
        public Sensor[] Measurements { get; set; }
        public PageDetails Page { get; set; }
    }
}