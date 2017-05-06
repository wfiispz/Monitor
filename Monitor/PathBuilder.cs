using System;

namespace Monitor
{
    //todo tests
    class PathBuilder : IPathBuilder
    {
        private readonly string _urlBasePath;

        public PathBuilder(string urlBasePath)
        {
            _urlBasePath = urlBasePath;
        }

        public string CreateForSensor(Guid guid)
        {
            return $"{_urlBasePath}/measurements/{guid}";
        }
    }
}