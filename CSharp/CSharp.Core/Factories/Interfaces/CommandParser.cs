using CSharp.Core.Commands.Interfaces;
using CSharpFunctionalExtensions;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Maybe<Command> Parse(string cmdString);
    }
}