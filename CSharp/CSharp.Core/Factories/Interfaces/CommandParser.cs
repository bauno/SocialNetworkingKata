using CSharp.Core.Commands.Interfaces;
using Optional;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Option<Command, string> Parse(string cmdString);
    }
}