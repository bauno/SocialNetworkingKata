using CSharp.Core.Commands.Interfaces;
using CSharpFunctionalExtensions;
using Optional;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Option<Command> Parse(string cmdString);
    }
}