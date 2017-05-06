using System.Linq;
using AutoMapper;
using Monitor.Database;

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
                    cfg.CreateMap<Resource, Modules.Resources.Get.Resource>()
                        .ForMember(x => x.Measurements,
                            opt => opt.MapFrom(
                                src => src.Sensors.Select(sensor => _pathBuilder.CreateForSensor(sensor.Guid))))
                        .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Guid));
                    cfg.CreateMap<Sensor, Modules.Measurements.Sensor>()
                        .ForMember(x => x.Host,
                            opt => opt.MapFrom(x => _pathBuilder.CreateForResource(x.Resource.Guid)))
                        .ForMember(x => x.Values, opt => opt.MapFrom(x => _pathBuilder.CreateForValues(x.Guid)));
                    cfg.CreateMap<Database.Measurement, Modules.Measurements.SensorValue>();
                });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}