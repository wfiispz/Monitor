using System.Linq;
using AutoMapper;
using Monitor.Database;

namespace Monitor.Mapping
{
    class AutomapperProvider
    {
        private readonly IPathBuilder _pathBuilder;

        public AutomapperProvider(IPathBuilder pathBuilder)
        {
            _pathBuilder = pathBuilder;
        }

        public IMapper Create()
        {
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<Resource, Modules.Resources.Get.Resource>()
                    .ForMember(x => x.Measurements,
                        opt => opt.MapFrom(
                            src => src.Sensors.Select(sensor => _pathBuilder.CreateForSensor(sensor.Guid))))
                    .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Guid)));
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
