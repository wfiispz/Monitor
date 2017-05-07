namespace Monitor.Api.Measurements.Query
{
    public class ValuesResponse
    {
        public string Measurements { get; set; }
        public SensorValue[] Values { get; set; }
    }
}