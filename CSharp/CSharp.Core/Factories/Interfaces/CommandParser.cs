using CSharp.Core.Commands.Interfaces;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Command Parse(string cmdString);
    }
}