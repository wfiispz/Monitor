using System;
using Autofac.Features.Indexed;
using Nancy;

namespace Monitor.CommandBus
{
    internal class SimpleBus : ICommandBus
    {
        private readonly IIndex<Type, IHandleCommand> _commandHandlersFactory;

        public SimpleBus(IIndex<Type, IHandleCommand> commandHandlersFactory)
        {
            _commandHandlersFactory = commandHandlersFactory;
        }

        public Response Handle<TCommand>(TCommand command) where TCommand : ICommand
        {
            var commandHandler = (IHandleCommand<TCommand>)_commandHandlersFactory[typeof(TCommand)];
            return commandHandler.Handle(command);
        }
    }
}