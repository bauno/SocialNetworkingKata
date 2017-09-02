using CSharp.Core.Services.Interfaces;
using LanguageExt;

namespace CSharp.Core.Commands.Interfaces
{
    public abstract class Command
    {
        public abstract Option<Displayable> SendTo(SocialNetwork socialNetwork);
    }
}