using System;
using System.Collections.Generic;
using System.Linq;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using LanguageExt;
using static LanguageExt.Prelude;
  

namespace CSharp.Core.Factories
{
    public class StringCommandParser : CommandParser
    {
        private readonly Arr<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            if (commandFactories == null) throw new ArgumentNullException(nameof(commandFactories));
            _commandFactories = commandFactories.ToArr();
        }

        private Either<string, Command> Parse(string cmdString, int factoryIndex)
        {                        
            return factoryIndex == _commandFactories.Count
                ? Left<string, Command>($"Cannot parse command: '{cmdString}'")
                : _commandFactories[factoryIndex]
                    .Parse(cmdString)
                    .Some(Right<string, Command>)
                    .None(() => Parse(cmdString, factoryIndex+1));
        }
        

        public Either<string, Command> Parse(string cmdString)
        {
            return Parse(cmdString, 0);
        }
    }
}