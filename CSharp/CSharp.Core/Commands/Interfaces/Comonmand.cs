using CSharp.Core.Services.Interfaces;

namespace CSharp.Core.Commands.Interfaces
{
    public interface Command
    {
        Command SendTo(SocialNetwork socialNetwork);
        void ShowOn(Display display);
    }
}