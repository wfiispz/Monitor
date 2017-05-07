using System.Linq;
using AutoMapper;
using Monitor.Api.Measurements.Query;
using Monitor.Database;
using Monitor.SensorCommunication.UpdateSensor;
using Sensor = Monitor.Database.Sensor;

namespace Monitor.Mapping
{
    internal class AutomapperProvider
    {
        private readonly IPathBuilder _pathBuilder;

        public AutomapperProvider(IPathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
        }

        public IMapper Create()
        {
            var config =
                new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Resource, Api.Resources.Query.Resource>()
                        .ForMember(x => x.Measurements,
                            opt => opt.MapFrom(
                                src => src.Sensors.Select(sensor => _pathBuilder.CreateForSensor(sensor.Guid))))
                        .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Guid));
                    cfg.CreateMap<Sensor, Api.Measurements.Query.Sensor>()
                        .ForMember(x => x.Host,
                            opt => opt.MapFrom(x => _pathBuilder.CreateForResource(x.Resource.Guid)))
                        .ForMember(x => x.Values, opt => opt.MapFrom(x => _pathBuilder.CreateForValues(x.Guid)));
                    cfg.CreateMap<Database.Measurement, SensorValue>();
                    cfg.CreateMap<Metadata, UpdateResource>()
                        .ForMember(x => x.Sensors, opt => opt.MapFrom(x => x.MeasuresArray))
                        .ForMember(x => x.Guid, opt => opt.MapFrom(x => x.ResourceId));
                });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}