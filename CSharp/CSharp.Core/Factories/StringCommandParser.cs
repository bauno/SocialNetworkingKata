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
        private readonly Seq<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            if (commandFactories == null) throw new ArgumentNullException(nameof(commandFactories));
            _commandFactories = commandFactories.ToSeq();
        }
        

        private Either<string, Command> Parse(Seq<CommandFactory> commandFactories, string cmdString)
        {
            return commandFactories.Match(
                () => Left<string, Command>($"Cannot parse command: '{cmdString}'"),
                (x, xs) => x.Parse(cmdString)
                    .Some(Right<string, Command>)
                    .None(() => Parse(xs,cmdString )));
        }
        

        public Either<string, Command> Parse(string cmdString)
        {
            return Parse(_commandFactories, cmdString);
        }
    }
}