using CSharp.Core.Commands.Interfaces;
using CSharpFunctionalExtensions;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Maybe<Command> Parse(string cmdString);
    }
}