using CSharp.Core.Commands.Interfaces;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Command Parse(string cmdString);
    }
}