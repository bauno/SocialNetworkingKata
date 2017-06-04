using CSharp.Core.Commands.Interfaces;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Message Parse(string cmdString);
    }
}