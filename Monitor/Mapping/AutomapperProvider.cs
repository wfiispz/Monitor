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
                                src =>

                                    src.Sensors.SelectMany(
                                            sensor => sensor.ComplexMetrics.Select(complex => _pathBuilder
                                                .CreateForSensor(complex.Guid)))
                                        .Concat(src.Sensors.Select(sensor => _pathBuilder.CreateForSensor(sensor.Guid)))
                                        .ToArray()
                            ))
                        .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Guid));

                    cfg.CreateMap<Sensor, Api.Measurements.Query.Sensor>()
                        .ForMember(x => x.Host,
                            opt => opt.MapFrom(x => _pathBuilder.CreateForResource(x.Resource.Guid)))
                        .ForMember(x => x.Values, opt => opt.MapFrom(x => _pathBuilder.CreateForValues(x.Guid)));

                    cfg.CreateMap<Database.Measurement, SensorValue>();

                    cfg.CreateMap<Metadata, UpdateResource>()
                        .ForMember(x => x.Sensors, opt => opt.MapFrom(x => x.MeasuresArray))
                        .ForMember(x => x.Guid, opt => opt.MapFrom(x => x.ResourceId));

                    cfg.CreateMap<ComplexMetric, Api.Measurements.Query.Sensor>()
                        .ForMember(x => x.Host, opt => opt.MapFrom(src => _pathBuilder.CreateForResource(src.Sensor.Resource.Guid)))
                        .ForMember(x => x.Metric, opt => opt.MapFrom(src => src.Sensor.Metric))
                        .ForMember(x => x.Unit, opt => opt.MapFrom(src => src.Sensor.Unit))
                        .ForMember(x => x.Complex, opt => opt.MapFrom(src => true))
                        .ForMember(x => x.MaxValue, opt => opt.MapFrom(src => src.Sensor.MaxValue))
                        .ForMember(x => x.Values, opt => opt.MapFrom(src => _pathBuilder.CreateForValues(src.Guid)));
                });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}