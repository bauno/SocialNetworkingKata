using CSharp.Core.Commands.Interfaces;
using LanguageExt;

namespace CSharp.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Option<Command> Parse(string cmdString);
    }
}