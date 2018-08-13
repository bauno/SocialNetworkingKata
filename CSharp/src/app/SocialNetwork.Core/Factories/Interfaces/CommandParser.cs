using SocialNetwork.Core.Commands.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Factories.Interfaces
{
    public interface CommandParser
    {
        Either<string, Command> Parse(string cmdString);
    }
}