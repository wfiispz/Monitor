using Nancy;

namespace Monitor.Modules
{
    public class IndexModule : NancyModule
    {
//        private readonly IRepeater _repeater;

        public IndexModule()//IRepeater repeater)
        {
//            _repeater = repeater;
            Get["/"] = parameters => "Its working!!!";
//            Get["/{value}"] = parameters => _repeater.Repeat(parameters.value);
        }
    }
}