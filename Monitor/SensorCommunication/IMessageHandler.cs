namespace Monitor.SensorCommunication
{
    public interface IMessageHandler
    {
        void Handle(string message);
    }
}