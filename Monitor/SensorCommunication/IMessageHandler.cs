namespace Monitor.SensorCommunication
{
    internal interface IMessageHandler
    {
        void Handle(string message);
    }
}