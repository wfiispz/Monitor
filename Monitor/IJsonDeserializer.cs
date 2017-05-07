namespace Monitor
{
    internal interface IJsonDeserializer
    {
        T Deserialize<T>(string value);
    }
}