using System.Collections.Generic;
using System.ComponentModel;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using LanguageExt;

namespace CSharp.Core.Factories
{
    public class StringCommandParser : CommandParser
    {
        private readonly IEnumerable<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            _commandFactories = commandFactories;
        }

        public Either<string, Command> Parse(string cmdString)
        {
            foreach (var commandFactory in _commandFactories)
            {
                var cmd = commandFactory
                    .Parse(cmdString)
                    .Some(c => c)
                    .None(() => new InvalidCommand());
                if (cmd.Type != CommandType.Invalid) return cmd;
            }
            return $"Cannot parse command: {cmdString}";
        }
    }
}