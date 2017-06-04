using CSharp.Core.Commands.Interfaces;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Message Parse(string cmdString);
    }
}