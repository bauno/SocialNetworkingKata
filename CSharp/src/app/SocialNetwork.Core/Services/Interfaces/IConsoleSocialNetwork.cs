using LanguageExt;

namespace SocialNetwork.Core.Services.Interfaces
{
    public interface IConsoleSocialNetwork
    {
        Option<string> Enter(string cmdString);
    }
}