using System;
using Monitor.Api.Auth;
using Monitor.Api.Index;
using Monitor.CommandBus;
using Nancy;
using Nancy.Security;

namespace Monitor.Api.Modules
{
    public class IndexModule : NancyModule
    {
        private readonly IRepeater _repeater;
        private ICommandBus _commandBus;

        public IndexModule(IRepeater repeater, ICommandBus commandBus)
        {
            this.RequiresAuthentication();
            this.RequiresClaims(AccessRights.Access);

            _repeater = repeater;
            _commandBus = commandBus;
            Get["/"] = parameters => "Its working!!!";
            Get["/{value}"] = parameters => _repeater.Repeat(parameters.value);
            Get["/oopserror"] = _ => throw new ArgumentException("message");
            Post["/{value}"] = parameters =>
            {
                var command = new IndexCommand(parameters.value);
                return _commandBus.Handle(command);
            };
            OnError += HandleException;
        }

        private object HandleException(NancyContext arg1, Exception arg2)
        {
            if (arg2 is ArgumentException)
            {
                return new Response {StatusCode = HttpStatusCode.BadRequest};
            }
            return new Response {StatusCode = HttpStatusCode.InternalServerError};
        }
    }
}