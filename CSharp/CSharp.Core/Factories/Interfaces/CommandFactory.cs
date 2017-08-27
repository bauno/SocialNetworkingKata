using CSharp.Core.Commands.Interfaces;
using LanguageExt;

namespace CSharp.Core.Factories.Interfaces
{
    public abstract class CommandFactory
    {
        public abstract Option<Command> Parse(string cmdString);
    }
}