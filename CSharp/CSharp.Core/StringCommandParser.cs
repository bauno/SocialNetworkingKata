using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace CSharp.Core
{
    public class StringCommandParser : CommandParser
    {
        private readonly IEnumerable<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            if (commandFactories == null) throw new ArgumentNullException(nameof(commandFactories));
            _commandFactories = commandFactories;
        }

        public Command Parse(string cmdString)
        {
            foreach (var commandFactory in _commandFactories)
            {
                var command = commandFactory.Parse(cmdString);
                if (command != null) return command;
            }
            throw new InvalidCommandException(cmdString);
        }
    }
}