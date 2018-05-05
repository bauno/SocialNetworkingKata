using System;
using System.Collections.Generic;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using LanguageExt;
using static LanguageExt.Prelude;
  

namespace CSharp.Core.Factories
{
    public class StringCommandParser : CommandParser
    {
        private readonly IEnumerable<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            _commandFactories = commandFactories ?? throw new ArgumentNullException(nameof(commandFactories));
        }
              
        private Either<string, Command> Parse(ISeq<CommandFactory> commandFactories, string cmdString)
        {
            return commandFactories.Match(
                () => $"Cannot parse command: '{cmdString}'",
                (x, xs) => x.Parse(cmdString)
                    .Some(Right<string, Command>)
                    .None(() => Parse(xs, cmdString)));
        }
        
        public Either<string, Command> Parse(string cmdString)
        {
            return Parse(_commandFactories.ToSeq(), cmdString);
        }
    }    
}