using System;
using System.Collections.Generic;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Exceptions;
using CSharp.Core.Factories.Interfaces;
using CSharpFunctionalExtensions;

namespace CSharp.Core.Factories
{
    public class StringCommandParser : CommandParser
    {
        private readonly IEnumerable<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            _commandFactories = commandFactories;
        }

        public Maybe<Command> Parse(string cmdString)
        {
            foreach (var commandFactory in _commandFactories)
            {
                var command = commandFactory.Parse(cmdString);
                if (command.HasValue) return command;
            }
            return null;
        }
    }
}