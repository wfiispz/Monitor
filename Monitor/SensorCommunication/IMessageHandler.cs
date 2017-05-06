using Monitor.SensorCommunication.Dto;

namespace Monitor.SensorCommunication
{
    interface IMessageHandler
    {
        DataType SupportedType { get; }
        void Handle(string message);
    }
}