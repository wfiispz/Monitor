using System;
using Autofac;

namespace Monitor.CommandBus
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal class CommandHandlerAttribute : Attribute
    {
        public Type CommandType { get; }

        public CommandHandlerAttribute(Type commandType)
        {
            CommandType = commandType;
            if (commandType.IsAssignableTo<ICommand>() == false)
                throw new ArgumentException("command type must implement ICommand interface");
        }
    }
}