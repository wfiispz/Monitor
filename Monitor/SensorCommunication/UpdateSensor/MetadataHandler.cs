using AutoMapper;
using Monitor.CommandBus;

namespace Monitor.SensorCommunication.UpdateSensor
{
    class MetadataHandler : ISingleMessageTypeHandler
    {
        private readonly IJsonDeserializer _jsonDeserializer;
        private readonly ICommandBus _commandBus;
        private readonly IMapper _mapper;

        public MetadataHandler(IJsonDeserializer jsonDeserializer, ICommandBus commandBus, IMapper mapper)
        {
            _jsonDeserializer = jsonDeserializer;
            _commandBus = commandBus;
            _mapper = mapper;
        }

        public DataType SupportedType => DataType.Metadata;

        public void Handle(string message)
        {
            var deserializedMessage = _jsonDeserializer.Deserialize<Metadata>(message);
            var updateResource = _mapper.Map<UpdateResource>(deserializedMessage);
            _commandBus.Handle(updateResource);
        }
    }
}
