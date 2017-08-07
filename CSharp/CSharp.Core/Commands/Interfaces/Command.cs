using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command
    {
        Maybe<Displayable> SendTo(SocialNetwork socialNetwork);
    }
}