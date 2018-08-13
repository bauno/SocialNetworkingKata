using SocialNetwork.Core.Services.Interfaces;
using LanguageExt;

namespace SocialNetwork.Core.Commands.Interfaces
{
    public interface Command
    {
        Option<Displayable> SendTo(ISocialNetwork socialNetwork);
    }
}