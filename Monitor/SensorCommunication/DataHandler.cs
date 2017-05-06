using Monitor.CommandBus;
using Monitor.SensorCommunication.Dto;

namespace Monitor.SensorCommunication
{
    internal class DataHandler : ISingleMessageTypeHandler
    {
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly ICommandBus _commandBus;

        public DataHandler(IJsonDeserializer jsonDeserializer, ICommandBus commandBus)
        {
            _jsonDeserializer = jsonDeserializer;
            _commandBus = commandBus;
        }

        public DataType SupportedType => DataType.Data;
        public void Handle(string message)
        {
            var deserializedMessage = _jsonDeserializer.Deserialize<Measurement>(message);

            foreach (var measurementValue in deserializedMessage.MesauresArray)
            {
                var addMeasurement = new AddMeasurement()
                {
                    Guid = measurementValue.MeasureId,
                    Timestamp = deserializedMessage.Timestamp,
                    Value = measurementValue.Value
                };

                _commandBus.Handle(addMeasurement);
            }

        }
    }
}