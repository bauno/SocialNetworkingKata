using SocialNetwork.Core.Commands.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories.Interfaces
{
    public interface CommandFactory
    {
        Option<Command> Parse(string cmdString);
    }
}