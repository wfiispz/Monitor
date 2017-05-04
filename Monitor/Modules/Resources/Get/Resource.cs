using System;

namespace Monitor.Modules.Resources.Get
{
    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Measurements { get; set; }
    }
}