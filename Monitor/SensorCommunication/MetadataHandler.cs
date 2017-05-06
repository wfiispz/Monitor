using Monitor.CommandBus;
using Monitor.SensorCommunication.Dto;

namespace Monitor.SensorCommunication
{
    class MetadataHandler : IMessageHandler
    {
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly ICommandBus _commandBus;

        public MetadataHandler(IJsonDeserializer jsonDeserializer, ICommandBus commandBus)
        {
            _jsonDeserializer = jsonDeserializer;
            _commandBus = commandBus;
        }

        public DataType SupportedType => DataType.Metadata;

        public void Handle(string message)
        {
            var deserializedMessage = _jsonDeserializer.Deserialize<Metadata>(message);

            foreach (var sensorDefinition in deserializedMessage.MeasuresArray)
            {
                var updateSensor = new UpdateSensor
                {
                    ResourceGuid = deserializedMessage.ResourceId,
                    SensorGuid = sensorDefinition.MeasureId,
                    Unit = sensorDefinition.Unit,
                    Metric = sensorDefinition.MeasureType
                };
                _commandBus.Handle(updateSensor);
            }
        }
    }
}
