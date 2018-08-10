using CSharp.Core.Commands.Interfaces;
using LanguageExt;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Either<string, Command> Parse(string cmdString);
    }
}