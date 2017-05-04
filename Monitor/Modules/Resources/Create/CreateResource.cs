﻿using Monitor.CommandBus;

namespace Monitor.Modules.Resources.Create
{
    public class CreateResource : ICommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Sensor[] Sensors { get; set; }
    }
}