using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
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
            if (commandFactories == null) throw new ArgumentNullException(nameof(commandFactories));
            _commandFactories = commandFactories;
        }
        

        private Either<string, Command> Parse(ISeq<CommandFactory> commandFactories, string cmdString)
        {
            return commandFactories.Match(
                () => Left<string, Command>($"Cannot parse command: '{cmdString}'"),
                (factory, remainder) => factory.Parse(cmdString)
                    .Some(Right<string, Command>)
                    .None(() => Parse(remainder,cmdString )));
        }
        
        public Either<string, Command> Parse(string cmdString)
        {
            return Parse(_commandFactories.ToSeq(), cmdString);
        }
    }
}