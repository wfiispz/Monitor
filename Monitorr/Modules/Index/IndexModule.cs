using Monitor.CommandBus;
using Nancy;

namespace Monitor.Modules.Index
{
    public class IndexModule : NancyModule
    {
        private readonly IRepeater _repeater;
        private ICommandBus _commandBus;

        public IndexModule(IRepeater repeater, ICommandBus commandBus)
        {
            _repeater = repeater;
            _commandBus = commandBus;
            Get["/"] = parameters => "Its working!!!";
            Get["/{value}"] = parameters => _repeater.Repeat(parameters.value);
            Get["/db/{value}"] = parameters => _repeater.FromDb(parameters.value);
            Post["/{value}"] = parameters =>
            {
                var command = new IndexCommand(parameters.value);
                return _commandBus.Handle(command);
            };

        }
    }
}