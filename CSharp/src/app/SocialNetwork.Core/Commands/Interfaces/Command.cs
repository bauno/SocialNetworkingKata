using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command
    {
        Option<Displayable> SendTo(SocialNetwork socialNetwork);
    }
}