namespace Monitor.SensorCommunication
{
    interface ISingleMessageTypeHandler
    {
        DataType SupportedType { get; }
        void Handle(string message);
    }
}