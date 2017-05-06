using System;
using Monitor.CommandBus;
using Monitor.Database;
using Nancy;
using Nancy.Bootstrapper;
using NHibernate;

namespace Monitor.Modules.Index
{
    public class IndexModule : NancyModule
    {
        private readonly IRepeater _repeater;
        private ICommandBus _commandBus;
        private ISessionFactory _sessionFactory;

        public IndexModule(IRepeater repeater, ICommandBus commandBus, ISessionFactory sessionFactory)
        {
            _repeater = repeater;
            _commandBus = commandBus;
            _sessionFactory = sessionFactory;
            Get["/"] = parameters => "Its working!!!";
            Get["/{value}"] = parameters => _repeater.Repeat(parameters.value);
            Get["/oopserror"] = _ => throw new ArgumentException("message");
            Get["/db/{value}"] = _ => _sessionFactory.OpenSession().QueryOver<Sensor>().List();
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