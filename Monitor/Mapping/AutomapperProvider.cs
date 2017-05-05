using System.Linq;
using AutoMapper;

namespace Monitor.Mapping
{
    class AutomapperProvider
    {
        public IMapper Create(string urlBasePath)
        {
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<Persistence.Resource, Modules.Resources.Get.Resource>()
                    .ForMember(x => x.Measurements,
                        opt => opt.MapFrom(
                            src => src.Sensors.Select(sensor => $"{urlBasePath}/resources/{sensor.Guid}"))));
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
