namespace Monitor.Modules.Measurements.Query
{
    public class ValuesResponse
    {
        public string Measurements { get; set; }
        public SensorValue[] Values { get; set; }
    }
}