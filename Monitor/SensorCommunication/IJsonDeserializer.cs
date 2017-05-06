namespace Monitor.SensorCommunication
{
    internal interface IJsonDeserializer
    {
        T Deserialize<T>(string value);
    }
}