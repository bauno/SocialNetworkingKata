using System.Collections.Generic;
using System.Data;
using CSharp.Core.Commands.Interfaces;
using CSharp.Core.Factories.Interfaces;
using Optional;

namespace CSharp.Core.Factories
{
    public static class MyExt
    {
        public static Option<T, TException> ToOption<T, TException>(this Option<T> value, TException exception) where T : struct
        {
            return value.Match(
                some: v => Option.Some<T, TException>(v),
                none: () => Option.None<T, TException>(exception));
        }
    }
    
    public class StringCommandParser : CommandParser
    {
        private readonly IEnumerable<CommandFactory> _commandFactories;

        public StringCommandParser(IEnumerable<CommandFactory> commandFactories)
        {
            _commandFactories = commandFactories;
        }

        public Option<Command, string> Parse(string cmdString)
        {
            foreach (var commandFactory in _commandFactories)
            {
                var command = commandFactory.Parse(cmdString)
                    .Match(
                    some: c => Option.Some<Command, string>(c),
                    none: () => Option.None<Command, string>($"Cannot parse command: {cmdString}"));
                if (command.HasValue) return command;
            }
            return Option.None<Command,string>($"Cannot parse command: {cmdString}");
        }
    }
}