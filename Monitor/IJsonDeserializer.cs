namespace Monitor
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string value);
    }
}