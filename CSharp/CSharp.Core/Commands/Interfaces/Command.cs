using CSharp.Core.Services.Interfaces;
using CSharpFunctionalExtensions;
using Optional;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command
    {
        Option<Displayable> SendTo(SocialNetwork socialNetwork);
    }
}