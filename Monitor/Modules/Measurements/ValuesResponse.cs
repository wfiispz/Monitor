namespace Monitor.Modules.Measurements
{
    public class ValuesResponse
    {
        public string Measurements { get; set; }
        public SensorValue[] Values { get; set; }
    }
}