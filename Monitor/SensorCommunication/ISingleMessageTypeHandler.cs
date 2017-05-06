using Monitor.SensorCommunication.Dto;

namespace Monitor.SensorCommunication
{
    interface ISingleMessageTypeHandler
    {
        DataType SupportedType { get; }
        void Handle(string message);
    }
}