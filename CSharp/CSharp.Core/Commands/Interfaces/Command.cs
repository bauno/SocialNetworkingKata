using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command
    {
        Displayable SendTo(SocialNetwork socialNetwork);
    }
}