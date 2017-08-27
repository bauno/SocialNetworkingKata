using System;
using System.Collections.Generic;
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
            if (commandFactories == null) throw new ArgumentNullException(nameof(commandFactories));
            _commandFactories = commandFactories;
        }

        public Either<string, Command> Parse(string cmdString)
        {
            foreach (var commandFactory in _commandFactories)
            {   
                Command cmd  = new InvalidCommand();
                commandFactory
                    .Parse(cmdString)
                    .IfSome(c => cmd = c);
                    
                if (cmd.Type != CommandType.Invalid) return cmd;
            }
            return $"Cannot parse command: '{cmdString}'";
        }
    }
}